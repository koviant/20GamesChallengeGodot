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
	var total_width = data.column_count * 100 + (data.column_count - 1) * 20
	var total_height = data.row_count * 30 + (data.row_count - 1) * 20
	var start_x = (get_viewport().get_visible_rect().size.x - total_width) / 2
	var start_y := 100
	var brick_size := Vector2(100, 30)
	for row in range(data.row_count):
		for column in range(data.column_count):
			var brick_data = data.cell_at(row, column)
			var brick := Brick.create(brick_size, brick_data.color)
			brick.position = Vector2(
				start_x + column * (100 + 20),
				start_y + row * (30 + 20))
			
			add_child(brick)
		
		
func remove_brick(brick: Brick) -> void:
	brick.queue_free()
	brick_count -= 1
