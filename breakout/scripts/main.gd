class_name BreakoutMain extends Node

@export var debug_ball_puddle_collision := true
@export var enable_normal_ball := false
@export var start_ball_vertically := false
@export var start_timeout_override_seconds := -1.0
@export var skip_death_animation := false
@export_range (1, 10) var max_life_count := 3

var ball_scene: PackedScene = preload("res://scenes/ball.tscn")
var debug_paddle_collision_balls: Array[Ball] = []

var ball_start_position: Vector2
var paddle_start_position: Vector2

@onready var main_ball: Ball = %Ball
@onready var paddle: Paddle = %Paddle
@onready var start_timer: Timer = %StartTimer
@onready var bricks_grid: BrickGrid = %BricksGrid
@onready var hud: CanvasLayer = %HUD

func _ready() -> void:
	paddle_start_position = Vector2(
		get_viewport().get_visible_rect().size.x / 2 - paddle.size.x / 2,
		get_viewport().get_visible_rect().size.y - 200)
	
	ball_start_position = Vector2(
		paddle_start_position.x + paddle.size.x / 2 - main_ball.size.x / 2,
		paddle_start_position.y - main_ball.size.y - 1)
	
	Lives.hud_layer = hud
	Lives.heart_start_position = Vector2(100, paddle_start_position.y + 120)
		
	bricks_grid.cleared.connect(next_level)
	
	game_reset()
	_check_debug()

func game_reset() -> void:
	Levels.reset()
	next_level()
	
	Lives.max_life_count = max_life_count
	Lives.reset()

func level_reset() -> void:
	set_physics_process(false)
	bricks_grid.reset()
	paddle_and_ball_reset()
	start_timer.start(start_timeout_override_seconds)

func paddle_and_ball_reset() -> void:
	if start_ball_vertically:
		main_ball.reset(ball_start_position, -90)
	else:
		main_ball.reset(ball_start_position)
	
	paddle.global_transform.origin = paddle_start_position

func next_level() -> void:
	if Levels.has_next():
		bricks_grid.data = Levels.next()
		level_reset()

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
		ball.current_velocity = ball.current_velocity.bounce(collision.get_normal())
		bricks_grid.remove_brick(collider)
	elif collider is Paddle:
		var total_possible_collision_len = paddle.size.x + 2 * ball.size.x
		var intersection_fraction: float = (collision.get_position().x - (paddle.position.x - ball.size.x)) / total_possible_collision_len
		var bounce_angle_deg = -135 + 90 * intersection_fraction
		ball.current_velocity = ball.current_velocity.rotated(deg_to_rad(bounce_angle_deg) - ball.current_velocity.angle())
	elif collider is DeathWall:
		await handle_death()
	else:
		ball.current_velocity = ball.current_velocity.bounce(collision.get_normal())

func handle_death():
	await decrease_life_with_animation()
	if not Lives.alive:
		game_reset()
	else:
		paddle_and_ball_reset()
	
func decrease_life_with_animation():
	set_physics_process(false)
	await Lives.decrease_life_count()
	set_physics_process(true)

func _check_debug() -> void:
	Lives.skip_animation = skip_death_animation
	
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
