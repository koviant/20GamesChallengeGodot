extends Node

const player_speed: int = 400;
var viewport_size: Vector2

const ball_speed_base = Vector2(400, 0)
var ball_speed = Vector2.ZERO

var ball_start_position: Vector2

const max_score: int = 5
var score_left: int = 0
var score_right: int = 0

enum Player { LEFT, RIGHT }
var lastScored: Player

var round_started: bool = false

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	$PlayerRight.color = Color.RED
	viewport_size = get_viewport().get_visible_rect().size
	ball_start_position = get_viewport().get_visible_rect().get_center() + $Ball.size / 2
	lastScored = Player.LEFT if randf() < 0.5 else Player.RIGHT
	_reset()


func _reset() -> void:
	$HUD.reset()
	score_left = 0
	score_right = 0
	_start_round()


func _start_round() -> void:
	round_started = false
	$Ball.position = ball_start_position
	$RoundStartTimer.start()


func _on_round_start_timer_timeout() -> void:
	var base_rotation = 135 if lastScored == Player.LEFT else -45
	var rotation = base_rotation + 90 * randf()
	ball_speed = ball_speed_base.rotated(deg_to_rad(rotation))
	round_started = true


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if not round_started:
		return
	
	_process_player($PlayerLeft, "p1_up", "p1_down", delta)
	_process_player($PlayerRight, "p2_up", "p2_down", delta)
	_process_ball(delta)


func _process_player(player: ColorRect, action_up: String, action_down: String, delta: float) -> void:
	if Input.is_action_pressed(action_up):
		player.position.y -= player_speed * delta
		player.position.y = clampf(player.position.y, 0, viewport_size.y - player.size.y)
	elif Input.is_action_pressed(action_down):
		player.position.y += player_speed * delta
		player.position.y = clampf(player.position.y, 0, viewport_size.y - player.size.y)


func _process_ball(delta: float) -> void:
	if $Ball.position.x <= 0:
		_add_right_score()
		return
		
	if $Ball.position.x + $Ball.get_size().x >= viewport_size.x:
		_add_left_score()
		return

	var new_position = $Ball.position + ball_speed * delta
	
	# Left player collision
	if ball_speed.x < 0 and new_position.x <= $PlayerLeft.position.x + $PlayerLeft.size.x:
		# Check intersection of path for top left ball corner and right side of a left player
		var intersection = _line_intersection(
			$Ball.position, 
			new_position, 
			Vector2($PlayerLeft.position.x + $PlayerLeft.size.x, $PlayerLeft.position.y - $Ball.size.y), 
			Vector2($PlayerLeft.position.x + $PlayerLeft.size.x, $PlayerLeft.position.y + $PlayerLeft.size.y))
		
		# Intersection is INF when intersection point is outside the player
		if intersection != Vector2.INF:
			ball_speed.x *= -1.1
			ball_speed.y *= 1.1
			$Ball.position = Vector2(intersection.x + 1, intersection.y)
			return
		
	# Right player collision
	elif ball_speed.x > 0 and new_position.x + $Ball.size.x >= $PlayerRight.position.x:
		# Check intersection of path for top right ball corner and left side of a right player
		var intersection = _line_intersection(
			Vector2($Ball.position.x + $Ball.size.x, $Ball.position.y), 
			Vector2(new_position.x + $Ball.size.x, $Ball.position.y), 
			Vector2($PlayerRight.position.x, $PlayerRight.position.y - $Ball.size.y), 
			Vector2($PlayerRight.position.x, $PlayerRight.position.y + $PlayerRight.size.y))
		
		# Intersection is INF when intersection point is outside the player
		if intersection != Vector2.INF:
			ball_speed.x *= -1.1
			ball_speed.y *= 1.1
			$Ball.position = Vector2(intersection.x - $Ball.size.x, intersection.y)
			return
	
	# Top border
	if new_position.y <= 0:
		var intersection = _line_intersection($Ball.position, new_position, Vector2(0, 0), Vector2(viewport_size.x, 0))
		ball_speed.y *= -1
		$Ball.position = Vector2(intersection.x, intersection.y + 1)
		
	# Bottom border
	elif new_position.y + $Ball.size.y >= viewport_size.y:
		var intersection = _line_intersection(
			Vector2($Ball.position.x, $Ball.position.y + $Ball.size.y),
			Vector2(new_position.x, new_position.y + $Ball.size.y), 
			Vector2(0, viewport_size.y), Vector2(viewport_size.x, viewport_size.y))
		
		ball_speed.y *= -1
		$Ball.position = Vector2(intersection.x, intersection.y - $Ball.size.y - 1)
		
	else:
		$Ball.position += ball_speed * delta


# Line intersection check required to avoid the situation when ball with high speed 
# teleports behind the player in one tick, or teleports far away outside the screen
func _line_intersection(p1: Vector2, p2: Vector2, q1: Vector2, q2: Vector2) -> Vector2:
	var dx_p = p2.x - p1.x
	var dy_p = p2.y - p1.y
	var dx_q = q2.x - q1.x
	var dy_q = q2.y - q1.y

	var denominator = dx_p * dy_q - dy_p * dx_q

	var u = ((q1.x - p1.x) * dy_p - (q1.y - p1.y) * dx_p) / denominator
	
	# Check that intersection is inside the q1->q2 segment
	if u < 0 or u > 1:
		return Vector2.INF
	
	var t = ((q1.x - p1.x) * dy_q - (q1.y - p1.y) * dx_q) / denominator

	return Vector2(p1.x + t * dx_p, p1.y + t * dy_p)


func _add_right_score():
	score_right += 1
	if score_right == max_score:
		$HUD.right_info_text = str(score_right) + ", WIN"
		$HUD.show_restart()
		round_started = false
		return
		
	$HUD.right_info_text = str(score_right)
	lastScored = Player.RIGHT
	_start_round()


func _add_left_score():
	score_left += 1
	if score_left == max_score:
		$HUD.left_info_text = str(score_left) + ", WIN"
		$HUD.show_restart()
		round_started = false
		return
		
	$HUD.left_info_text = str(score_left)
	lastScored = Player.LEFT
	_start_round()
