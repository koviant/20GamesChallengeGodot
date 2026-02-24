extends RigidBody2D


var size: Vector2:
	get: return ($CollisionShape2D.shape as RectangleShape2D).size

const base_velocity := Vector2(300, 0)
var velocity: Vector2

func reset(pos: Vector2) -> void:
	global_position = pos
	var speed_rotation = -135 + 90 * randf()
	velocity = base_velocity.rotated(speed_rotation)
	
func start() -> void:
	set_physics_process(true)

func stop() -> void:
	set_physics_process(false)

func _physics_process(delta: float) -> void:
	var collision = move_and_collide(velocity * delta)
	
	if collision:
		velocity = velocity.bounce(collision.get_normal())
		
