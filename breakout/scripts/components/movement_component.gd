class_name MovementComponent extends Node

const paddle_velocity := 800.0

@onready var paddle: Paddle = get_parent()

func _physics_process(_delta: float) -> void:
	if not is_instance_valid(paddle):
		return
	
	var direction := Input.get_axis("ui_left", "ui_right")
	paddle.horizontal_velocity = direction * paddle_velocity
