@tool
class_name Ball extends RigidBody2D

var size: Vector2:
	get: return $CollisionShape2D.shape.get_rect().size
	set(value):
		var shape := RectangleShape2D.new()
		shape.size = value
		$CollisionShape2D.shape = shape
		queue_redraw()

var initial_velocity := Vector2(600, 0)
var current_velocity: Vector2

func _draw() -> void:
	var center: Vector2 = $CollisionShape2D.position
	var radius := size.x / 2
	draw_circle(center, radius, Color.WHITE)
	
func reset(pos: Vector2, angle_deg: float = NAN) -> void:
	global_position = pos
	var speed_rotation = angle_deg if not is_nan(angle_deg) else -135 + 90 * randf()
	current_velocity = initial_velocity.rotated(deg_to_rad(speed_rotation))
