class_name HeartDisplayComponent extends Node

const _HEART_EMPTY: Texture2D = preload("uid://bpjx44406nhxe")
const _HEART_FULL: Texture2D = preload("uid://c3xjcawoynrv5")
const _heart_offset := 8

var _max_life_count: int
var _life_count: int

var hud_layer: CanvasLayer
var heart_start_position: Vector2
var empty_hearts: Array[Sprite2D] = []
var full_hearts: Array[Sprite2D] = []

func reset(max_life_count: int):
	_life_count = max_life_count
	if _max_life_count != max_life_count or _sprites_not_created():
		_max_life_count = max_life_count
		_create_sprites()
	
	_reset_all_hearts_scale()

func _reset_all_hearts_scale():
	for heart in full_hearts:
		heart.scale = Vector2.ONE

func _sprites_not_created() -> bool:
	return len(full_hearts) == 0

func _create_sprites() -> void:
	full_hearts.clear()
	empty_hearts.clear()
	var current_position = heart_start_position
	for i in range(_max_life_count):
		var full_heart := _create_sprite(_HEART_FULL, current_position)
		var empty_heart := _create_sprite(_HEART_EMPTY, current_position)
		
		current_position.x += full_heart.texture.get_width() + _heart_offset
		
		full_hearts.append(full_heart)
		empty_hearts.append(empty_heart)
		
		hud_layer.add_child(full_heart)
		hud_layer.add_child(empty_heart)

func _create_sprite(texture: Texture2D, pos: Vector2) -> Sprite2D: 
	var heart := Sprite2D.new()
	heart.texture = texture
	heart.position = pos
	
	return heart
