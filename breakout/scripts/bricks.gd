extends Node

@export var rows := 8
@export var colums := 5
@export var h_spacing := 10
@export var v_spacing := 10
@export var brick_width := 150
@export var brick_height := 50

var brickScene = preload("res://scenes/brick.tscn")

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	_create_bricks()

func _create_bricks() -> void:
	var total_width = colums * brick_width + (colums - 1) * h_spacing
	var total_height = rows * brick_height + (rows - 1) * v_spacing
	
	assert(total_width > 0, "total_width < 0")
	assert(get_viewport().get_visible_rect().size.x > total_width, "total_width > viewport width")
	assert(total_height > 0, "total_height < 0")
	assert(get_viewport().get_visible_rect().size.y > total_height, "total_height > viewport height")
	
	var start_x = (get_viewport().get_visible_rect().size.x - total_width) / 2
	var start_y := 100
	var current_column := 0
	var brick_size := Vector2(brick_width, brick_height)
	while current_column <= colums:
		var current_row := 0
		while current_row <= rows:
			var brick := Brick.create(brick_size)
			brick.position = Vector2(
				start_x + current_column * (brick_width + h_spacing),
				start_y + current_row * (brick_height + v_spacing))
			
			print(brick.position)
			
			add_child(brick)
			current_row += 1
		
		current_column += 1
