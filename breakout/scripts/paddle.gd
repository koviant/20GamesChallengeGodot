class_name Paddle extends RigidBody2D

var size: Vector2:
	get: return ($CollisionShape2D.shape as RectangleShape2D).size

const paddle_velocity := 800

func _physics_process(delta: float) -> void:
	linear_velocity.x = _get_velocity_x()

func _get_velocity_x() -> float:
	if Input.is_action_pressed("ui_left"):
		return -paddle_velocity
	elif Input.is_action_pressed("ui_right"):
		return paddle_velocity

	return 0
