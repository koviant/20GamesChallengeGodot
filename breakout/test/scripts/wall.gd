class_name Wall extends StaticBody2D

var size: Vector2:
	get: return (collision.shape as RectangleShape2D).size
	set(value):
		var shape := RectangleShape2D.new()
		shape.size = value
		collision.shape = shape

@onready var collision: CollisionShape2D = %Collision
