extends Node

const _heart_empty: Texture2D = preload("uid://bpjx44406nhxe")
const _heart_full: Texture2D = preload("uid://c3xjcawoynrv5")

var max_life_count: int
var _life_count: int

var hearts_container: FlowContainer

var alive: bool: 
	get: return _life_count > 0
	
func reset():
	_life_count = max_life_count
	if not _can_fill_existing_texture_rects():
		_create_texture_rects()
	
	_fill_texture_rects_full_heart()
		
func _can_fill_existing_texture_rects() -> bool:
	return hearts_container.get_child_count() != 0

func _create_texture_rects() -> void:
	for i in range(max_life_count):
		var heart := TextureRect.new()
		hearts_container.add_child(heart)

func _fill_texture_rects_full_heart() -> void:
	for ch: TextureRect in hearts_container.get_children():
		ch.texture = _heart_full	 

func decrease_life_count():
	assert(_life_count >= 0, "calling decrease_life_count on invalid life count")
	
	_life_count -= 1
	_update_hearts()
		
func _update_hearts():
	if _life_count < max_life_count:
		var textureRect: TextureRect = hearts_container.get_child(_life_count)
		textureRect.texture = _heart_empty
