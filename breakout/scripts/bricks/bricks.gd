class_name BrickGrid extends Node

signal cleared

@export var data: BrickGridData:
	get: return data
	set(value):
		data = value

const NOT_SET := -1.0

const _start_y := 50
var _start_x := 0

var _brick_count: int = 0
var _total_width := NOT_SET

var show_bricks_coordinates := false

func reset():
	_clear_bricks()
	_create_bricks()
	
func _create_bricks() -> void:	
	var total_width = _get_total_width(true)
	var total_height = data.row_count * data.brick_height + (data.row_count - 1) * data.row_spacing
	
	assert(total_width > 0, "total_width < 0")
	assert(get_viewport().get_visible_rect().size.x > total_width, "total_width > viewport width")
	assert(total_height > 0, "total_height < 0")
	assert(get_viewport().get_visible_rect().size.y / 2 > total_height, "total_height > half of the viewport height")
	
	_start_x = _get_start_x()
	
	var brick_size := Vector2(data.brick_width, data.brick_height)
	for row in range(data.row_count):
		for column in range(data.column_count):
			var brick_data = data.cell_at(row, column)
			if brick_data.is_empty:
				continue
			
			_brick_count += 1
			var brick := Brick.create(brick_size, brick_data)
			brick.hit_handler = Util.create_hit_handler(self, brick, row, column)
			brick.position = _get_position(row, column)
			
			if show_bricks_coordinates:
				var label = Label.new()
				brick.add_child(label)
				label.text = str(brick.position)
				label.add_theme_font_size_override("font_size", 10)
				label.position = Vector2(-30, -10)
			
			add_child(brick)
	
func remove_brick(brick: Brick) -> void:
	brick.queue_free()
	_brick_count -= 1
	if _brick_count == 0:
		cleared.emit()

func get_at(row: int, col: int) -> Brick:
	var position := _get_position(row, col)
	for b: Brick in get_children():
		var rect = Rect2(b.global_position, b.size)
		if rect.has_point(position):
			return b
			
	return null

func _clear_bricks() -> void:
	for brick in get_children():
		brick.queue_free()

func _get_start_x() -> int:
	@warning_ignore("narrowing_conversion")
	return (get_viewport().get_visible_rect().size.x - _get_total_width()) / 2

func _get_position(row: int, col: int) -> Vector2:
	return Vector2(
		_start_x + col * (data.brick_width + data.column_spacing),
		_start_y + row * (data.brick_height + data.row_spacing))

func _get_total_width(force: bool = false) -> float:
	if _total_width != NOT_SET and not force:
		return _total_width
	
	_total_width = data.column_count * data.brick_width + (data.column_count - 1) * data.column_spacing
	return _total_width
