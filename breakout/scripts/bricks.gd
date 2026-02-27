class_name BrickGrid extends Node

signal bricks_cleared

@export var data: BrickGridData 

var brick_count: int:
	get: return brick_count
	set(value):
		brick_count = value
		if brick_count == 0:
			bricks_cleared.emit()

func reset():
	_create_bricks()
	
func _create_bricks() -> void:
	brick_count = data.row_count * data.column_count
	var total_width = data.column_count * data.brick_width + (data.column_count - 1) * data.column_spacing
	var total_height = data.row_count * data.brick_height + (data.row_count - 1) * data.row_spacing
	var start_x = (get_viewport().get_visible_rect().size.x - total_width) / 2
	var start_y := 100
	var brick_size := Vector2(data.brick_width, data.brick_height)
	for row in range(data.row_count):
		for column in range(data.column_count):
			var brick_data = data.cell_at(row, column)
			if brick_data.is_empty:
				continue
			
			var brick := Brick.create(brick_size, brick_data.color)
			brick.position = Vector2(
				start_x + column * (data.brick_width + data.column_spacing),
				start_y + row * (data.brick_height + data.row_spacing))
			
			add_child(brick)
		
		
func remove_brick(brick: Brick) -> void:
	brick.queue_free()
	brick_count -= 1
