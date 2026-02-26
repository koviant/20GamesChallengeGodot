extends Node

@export var debug_ball_puddle_collision := true
@export var enable_normal_ball := false

var ball_scene: PackedScene = preload("res://scenes/ball.tscn")
var debug_paddle_collision_balls: Array[Ball] = []

var ball_start_position: Vector2
var paddle_start_position: Vector2
var bricks_start_position: Vector2

@onready var main_ball: Ball = %Ball
@onready var paddle: Paddle = %Paddle
@onready var start_timer: Timer = %StartTimer
@onready var bricks_grid: BrickGrid = %BricksGrid

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	paddle_start_position = Vector2(
		get_viewport().get_visible_rect().size.x / 2 - paddle.size.x / 2,
		get_viewport().get_visible_rect().size.y - 100)
	
	ball_start_position = Vector2(
		paddle_start_position.x + paddle.size.x / 2 - main_ball.size.x / 2,
		paddle_start_position.y - main_ball.size.y - 1)
	
	bricks_start_position = Vector2(
		get_viewport().get_visible_rect().size.x * 0.05,
		100)
	
	reset()
	
	_check_debug()
	
func reset() -> void:
	set_physics_process(false)
	bricks_grid.reset()
	main_ball.reset(ball_start_position)
	paddle.position = paddle_start_position
	start_timer.start()

func _start_ball() -> void:
	set_physics_process(true)

func _physics_process(delta: float) -> void:
	if enable_normal_ball:	
		_process_ball_physics(main_ball, delta)
		
	if debug_ball_puddle_collision:
		for ball in debug_paddle_collision_balls:
			_process_ball_physics(ball, delta)
		

func _process_ball_physics(ball: Ball, delta: float) -> void:	
	
	var collision = ball.move_and_collide(ball.current_velocity * delta)
	
	if not collision:
		return
	
	var collider = collision.get_collider()
	if collider is Brick:
		bricks_grid.remove_brick(collider)
		ball.current_velocity = ball.current_velocity.bounce(collision.get_normal())
	elif collider is Paddle:
		var total_possible_collision_len = paddle.size.x + 2 * ball.size.x
		var intersection_fraction: float = (collision.get_position().x - (paddle.position.x - ball.size.x)) / total_possible_collision_len
		var bounce_angle_deg = -135 + 90 * intersection_fraction
		ball.current_velocity = ball.current_velocity.rotated(deg_to_rad(bounce_angle_deg) - ball.current_velocity.angle())
	else:
		ball.current_velocity = ball.current_velocity.bounce(collision.get_normal())

func _check_debug() -> void:
	if debug_ball_puddle_collision:
		var y := paddle.position.y - 100 
		var initial_velocity = Vector2(0, -80)
		var size := main_ball.size / 2
		var initial_x := paddle.position.x - size.x + 1
		var end_x := paddle.position.x + paddle.size.x
		var step_x := size.x + 5
		for x: int in range(initial_x, end_x, step_x):
			var ball: Ball = ball_scene.instantiate()
			ball.size = size
			ball.initial_velocity = initial_velocity
			add_child(ball)
			ball.reset(Vector2(x, y), 180)
			ball.set_collision_layer_value(2, true)
			ball.set_collision_mask_value(1, true)
			ball.set_collision_mask_value(2, false)
			ball.set_collision_mask_value(3, true)
			ball.set_collision_mask_value(4, true)
			
			debug_paddle_collision_balls.append(ball)
