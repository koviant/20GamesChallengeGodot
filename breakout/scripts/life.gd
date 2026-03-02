extends Node

const _HEART_EMPTY: Texture2D = preload("uid://bpjx44406nhxe")
const _HEART_FULL: Texture2D = preload("uid://c3xjcawoynrv5")
const _heart_offset := 8
const _heart_animation_duration := 0.2
const _beating_heart_animation_duration := 0.1
const _animation_end_pause := 2

var max_life_count: int
var _life_count: int

var hud_layer: CanvasLayer
var heart_start_position: Vector2

var _full_hearts: Array[Sprite2D] = []
var _empty_hearts: Array[Sprite2D] = []

var _beating_last_heart_animation: Tween

var alive: bool: 
	get: return _life_count > 0

func reset():
	_life_count = max_life_count
	if not _sprites_created():
		_create_sprites()
		_create_beating_heart_animation()
	else:
		_reset_full_heart_size()
		
func _sprites_created() -> bool:
	return len(_full_hearts) > 0

func _create_sprites() -> void:
	var current_position = heart_start_position
	for i in range(max_life_count):
		var full_heart := _create_sprite(_HEART_FULL, current_position)
		var empty_heart := _create_sprite(_HEART_EMPTY, current_position)
		
		current_position.x += full_heart.texture.get_width() + _heart_offset
		
		_full_hearts.append(full_heart)
		_empty_hearts.append(empty_heart)
		
		hud_layer.add_child(full_heart)
		hud_layer.add_child(empty_heart)

func _create_sprite(texture: Texture2D, pos: Vector2) -> Sprite2D:
	var heart := Sprite2D.new()
	heart.texture = texture
	heart.position = pos
	
	return heart

func _reset_full_heart_size() -> void:
	for heart in _full_hearts:
		heart.scale = Vector2.ONE

func decrease_life_count():
	assert(_life_count >= 0, "calling decrease_life_count on invalid life count")
	
	_life_count -= 1
	
	var heart_lost_tween = _create_heart_lost_animation_tween()
	
	if _life_count == 1:
		_beating_last_heart_animation.play()
	
	if _life_count == 0:
		_beating_last_heart_animation.stop()
		heart_lost_tween.tween_subtween(_create_lives_end_animation_tween())
		
	heart_lost_tween.tween_interval(_animation_end_pause)
	
	await heart_lost_tween.finished

func _create_heart_lost_animation_tween() -> Tween:
	var tween = create_tween()
	tween.tween_property(_full_hearts[_life_count], "scale", Vector2.ZERO, _heart_animation_duration)
	
	return tween

func _create_lives_end_animation_tween() -> Tween:
	var tween = create_tween().set_loops(5)
	tween.tween_callback(_hide_all_empty_hearts)\
		.set_delay(_heart_animation_duration)
		
	tween.tween_callback(_show_all_empty_hearts)\
		.set_delay(_heart_animation_duration)
	
	return tween
	
func _hide_all_empty_hearts() -> void:
	for empty in _empty_hearts:
		empty.hide()

func _show_all_empty_hearts() -> void:
	for empty in _empty_hearts:
		empty.show()

func _create_beating_heart_animation() -> void:
	var h = _full_hearts[0]
	
	var tween = create_tween().set_loops()
	tween.tween_interval(_heart_animation_duration)
	tween.tween_property(h, "scale", Vector2.ONE * 0.7, _beating_heart_animation_duration)
	tween.tween_interval(_beating_heart_animation_duration)
	tween.tween_property(h, "scale", Vector2.ONE, _beating_heart_animation_duration)
	tween.stop()
	
	_beating_last_heart_animation = tween
