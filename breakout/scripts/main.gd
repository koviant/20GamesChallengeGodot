extends Node

var ball_start_position: Vector2
var paddle_start_position: Vector2
var bricks_start_position: Vector2

@onready var ball: Ball = %Ball
@onready var paddle: Paddle = %Paddle
@onready var start_timer: Timer = %StartTimer
@onready var bricks_grid: BrickGrid = %BricksGrid

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	paddle_start_position = Vector2(
		get_viewport().get_visible_rect().size.x / 2 - paddle.size.x / 2,
		get_viewport().get_visible_rect().size.y - 100)
	
	ball_start_position = Vector2(
		paddle_start_position.x + paddle.size.x / 2 - ball.size.x / 2,
		paddle_start_position.y - ball.size.y)
	
	bricks_start_position = Vector2(
		get_viewport().get_visible_rect().size.x * 0.05,
		100)
	
	reset()
	
func reset() -> void:
	set_physics_process(false)
	bricks_grid.reset()
	ball.reset(ball_start_position)
	paddle.position = paddle_start_position
	start_timer.start()

func _start_ball() -> void:
	set_physics_process(true)

func _physics_process(delta: float) -> void:
	var collision = ball.move_and_collide(ball.current_velocity * delta)
	
	if not collision:
		return
	
	var collider = collision.get_collider()
	if collider is Brick:
		bricks_grid.remove_brick(collider)
		ball.current_velocity = ball.current_velocity.bounce(collision.get_normal())
		pass
	elif collider is Paddle:
		var total_possible_collision_len = paddle.size.x + 2 * ball.size.x
		var intersection_fraction: float = (collision.get_position().x - (paddle.position.x - ball.size.x)) / total_possible_collision_len
		var bounce_angle_deg = -135 + 90 * intersection_fraction
		ball.current_velocity = ball.current_velocity.rotated(deg_to_rad(bounce_angle_deg))
	else:
		ball.current_velocity = ball.current_velocity.bounce(collision.get_normal())
