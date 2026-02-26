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

func _physics_process(delta: float) -> void:
	var collision = $Ball.move_and_collide($Ball.current_velocity * delta)
	
	if not collision:
		return
	
	if collision.get_collider() is Brick:
		collision.get_collider().queue_free()
		$Ball.current_velocity = $Ball.current_velocity.bounce(collision.get_normal())
	elif collision.get_collider() is Paddle:
		var paddle := collision.get_collider() as Paddle
		var total_possible_collision_len = paddle.size.x + 2 * $Ball.size.x
		var intersection_fraction: float = (collision.get_position().x - (paddle.position.x - $Ball.size.x)) / total_possible_collision_len
		var bounce_angle_deg = -135 + 90 * intersection_fraction
		$Ball.current_velocity = $Ball.base_current_velocity.rotated(deg_to_rad(bounce_angle_deg))
	else:
		$Ball.current_velocity = $Ball.current_velocity.bounce(collision.get_normal())
