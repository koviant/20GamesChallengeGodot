extends RigidBody2D


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	linear_velocity = Vector2(100, 0).rotated(deg_to_rad(-135 + 90 * randf()))

func _physics_process(delta: float) -> void:
	var collision = move_and_collide(linear_velocity)
	
	if collision:
		linear_velocity = linear_velocity.bounce(collision.get_normal())
		
