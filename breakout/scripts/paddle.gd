class_name Paddle extends RigidBody2D

var size: Vector2:
	get: return ($CollisionShape2D.shape as RectangleShape2D).size

const paddle_velocity := 400
const max_paddle_velocity := 400
const acceleration := 400

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.

func _physics_process(delta: float) -> void:
	#linear_velocity.x = _get_velocity_x()
	var direction = Input.get_axis("ui_left", "ui_right")
	if direction == 0:
		linear_velocity.x = move_toward(linear_velocity.x, 0, acceleration / 50)
		
	linear_velocity.x += direction * acceleration * delta

func _get_velocity_x() -> float:
	if Input.is_action_pressed("ui_left"):
		return -paddle_velocity
	elif Input.is_action_pressed("ui_right"):
		return paddle_velocity

	return 0

func _get_acc_x() -> float:
	if Input.is_action_pressed("ui_left"):
		return -acceleration
	elif Input.is_action_pressed("ui_right"):
		return acceleration

	return 0
