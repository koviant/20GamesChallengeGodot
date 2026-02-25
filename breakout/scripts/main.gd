extends Node


var ball_start_position: Vector2
var paddle_start_position: Vector2
var bricks_start_position: Vector2


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	paddle_start_position = Vector2(
		get_viewport().get_visible_rect().size.x / 2 - $Paddle.size.x / 2,
		get_viewport().get_visible_rect().size.y - 150)
	
	ball_start_position = Vector2(
		get_viewport().get_visible_rect().size.x / 2 - $Ball.size.x / 2,
		paddle_start_position.y - 400)
	
	bricks_start_position = Vector2(
		get_viewport().get_visible_rect().size.x * 0.05,
		100)
	
	reset()
	
func reset() -> void:
	$Ball.stop()
	$Ball.reset(ball_start_position)
	$Paddle.position = paddle_start_position
	$StartTimer.start()

func _start_ball() -> void:
	$Ball.start()
