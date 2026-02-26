extends Node

signal bricks_cleared

var rows := 8
var colums := 25
var h_spacing := 10
var v_spacing := 10
var brick_width := 100
var brick_height := 50

var brick_count: int:
	get: return brick_count
	set(value):
		brick_count = value
		if brick_count == 0:
			bricks_cleared.emit()

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	_create_bricks()

func _create_bricks() -> void:
	brick_count = rows * colums
	var total_width = colums * brick_width + (colums - 1) * h_spacing
	var total_height = rows * brick_height + (rows - 1) * v_spacing
	
	assert(total_width > 0, "total_width < 0")
	assert(get_viewport().get_visible_rect().size.x > total_width, "total_width > viewport width")
	assert(total_height > 0, "total_height < 0")
	assert(get_viewport().get_visible_rect().size.y > total_height, "total_height > viewport height")
	
	var start_x = (get_viewport().get_visible_rect().size.x - total_width) / 2
	var start_y := 100
	var brick_size := Vector2(brick_width, brick_height)
	var row_color: Color
	for current_row in range(rows):
		if current_row % 2 == 0:
			row_color = Color(randf(), randf(), randf())
			print(row_color)
			
		for current_column in range(colums):
			var brick := Brick.create(brick_size, row_color)
			brick.position = Vector2(
				start_x + current_column * (brick_width + h_spacing),
				start_y + current_row * (brick_height + v_spacing))
			
			add_child(brick)
		
		
func remove_brick(brick: Brick):
	brick.queue_free()
	brick_count -= 1
