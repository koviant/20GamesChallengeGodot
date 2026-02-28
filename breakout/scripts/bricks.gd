class_name BrickGrid extends Node

signal cleared

@export var data: BrickGridData 

var _brick_count: int = 0

func reset():
	_create_bricks()
	
func _create_bricks() -> void:
	var total_width = data.column_count * data.brick_width + (data.column_count - 1) * data.column_spacing
	var total_height = data.row_count * data.brick_height + (data.row_count - 1) * data.row_spacing
	
	assert(total_width > 0, "total_width < 0")
	assert(get_viewport().get_visible_rect().size.x > total_width, "total_width > viewport width")
	assert(total_height > 0, "total_height < 0")
	assert(get_viewport().get_visible_rect().size.y / 2 > total_height, "total_height > half of the viewport height")
	
	var start_x = (get_viewport().get_visible_rect().size.x - total_width) / 2
	var start_y := 50
	var brick_size := Vector2(data.brick_width, data.brick_height)
	for row in range(data.row_count):
		for column in range(data.column_count):
			var brick_data = data.cell_at(row, column)
			if brick_data.is_empty:
				continue
			
			_brick_count += 1
			var brick := Brick.create(brick_size, brick_data.color)
			brick.position = Vector2(
				start_x + column * (data.brick_width + data.column_spacing),
				start_y + row * (data.brick_height + data.row_spacing))
			
			add_child(brick)
		
		
func remove_brick(brick: Brick) -> void:
	brick.queue_free()
	_brick_count -= 1
	if _brick_count == 0:
		cleared.emit()
