@tool
class_name Ball extends RigidBody2D

var size: Vector2:
	get: return $CollisionShape2D.shape.get_rect().size

const initial_velocity := Vector2(300, 0)
var current_velocity: Vector2

func _draw() -> void:
	var center := size.x / 2
	var radius := size.x / 2
	draw_circle(Vector2(center, center), radius, Color.WHITE)
	
func reset(pos: Vector2) -> void:
	global_position = pos
	var speed_rotation = -135 + 90 * randf()
	current_velocity = initial_velocity.rotated(deg_to_rad(speed_rotation))
