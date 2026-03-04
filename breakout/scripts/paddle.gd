class_name Paddle extends RigidBody2D

var size: Vector2:
	get: return ($CollisionShape2D.shape as RectangleShape2D).size

@onready var horizontal_velocity: float = 0.0: 
	set(value):
		if linear_velocity.x == value:
			return
		
		linear_velocity.x = value
