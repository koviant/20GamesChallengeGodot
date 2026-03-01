extends Node

const _HEART_EMPTY: Texture2D = preload("uid://bpjx44406nhxe")
const _HEART_FULL: Texture2D = preload("uid://c3xjcawoynrv5")
const _heart_offset := 8
const _heart_animation_duration := 0.2

var max_life_count: int
var _life_count: int

var hud_layer: CanvasLayer
var heart_start_position: Vector2

var _full_hearts: Array[TextureRect] = []
var _empty_hearts: Array[TextureRect] = []

var alive: bool: 
	get: return _life_count > 0

func reset():
	_life_count = max_life_count
	if not _can_fill_existing_texture_rects():
		_create_texture_rects()
	else:
		_reset_texture_rects_full_heart()
		
func _can_fill_existing_texture_rects() -> bool:
	return len(_full_hearts) > 0

func _create_texture_rects() -> void:
	var current_position = heart_start_position
	for i in range(max_life_count):
		var full_heart := _create_texture_rect(_HEART_FULL, current_position)
		var empty_heart := _create_texture_rect(_HEART_EMPTY, current_position)
		
		current_position.x += full_heart.size.x + _heart_offset
		
		_full_hearts.append(full_heart)
		_empty_hearts.append(empty_heart)
		
		hud_layer.add_child(full_heart)
		hud_layer.add_child(empty_heart)

func _create_texture_rect(texture: Texture2D, pos: Vector2) -> TextureRect:
	var heart := TextureRect.new()
	heart.texture = texture
	heart.position = pos
	
	return heart

func _reset_texture_rects_full_heart() -> void:
	for heart in _full_hearts:
		heart.scale = Vector2.ONE

func decrease_life_count():
	assert(_life_count >= 0, "calling decrease_life_count on invalid life count")
	
	_life_count -= 1
	
	var heart_lost_tween = _create_heart_lost_animation_tween()
	
	if _life_count == 0:
		heart_lost_tween.tween_subtween(_create_lives_end_animation_tween())
		
	heart_lost_tween.tween_interval(2)
	
	await heart_lost_tween.finished

func _create_heart_lost_animation_tween() -> Tween:
	var h = _full_hearts[_life_count]
	var initial_position = h.position
	var center_x = h.position.x + h.size.x / 2
	var center_y = h.position.y + h.size.y / 2
	var tween = create_tween()
	tween.set_parallel(true)
	tween.tween_property(h, "scale", Vector2.ZERO, _heart_animation_duration)
	tween.tween_property(h, "position", Vector2(center_x, center_y), _heart_animation_duration)
	tween.set_parallel(false)
	tween.tween_callback(func(): h.position = initial_position)
	
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
