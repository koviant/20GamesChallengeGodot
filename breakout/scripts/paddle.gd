class_name Paddle extends RigidBody2D

signal death_animation_finished

const ANIMATION_HEART_LOST := "HEART_LOST"
const ANIMATION_HEART_BEATING := "HEART_BEATING"
const ANIMATION_NONE_HEARTS_LEFT := "NONE_HEARTS_LEFT"

var skip_death_animation := false
var max_life_count: int

var size: Vector2:
	get: return ($CollisionShape2D.shape as RectangleShape2D).size

var alive: bool:
	get: return health_component.alive

@onready var health_component: HealthComponent = %HealthComponent
@onready var animation_component: AnimationComponent = %AnimationComponent
@onready var movement_component: MovementComponent = %MovementComponent
@onready var heart_display_component: HeartDisplayComponent = %HeartDisplayComponent
@onready var horizontal_velocity: float = 0.0: 
	set(value):
		if linear_velocity.x == value:
			return
			
		linear_velocity.x = value


func setup() -> void:
	var animation_dict : Dictionary[String, Callable] = {
		ANIMATION_HEART_LOST: _start_heart_lost_animation,
		ANIMATION_HEART_BEATING: _start_heart_beating_animation,
		ANIMATION_NONE_HEARTS_LEFT: _start_bliping_hearts_animation,
	}
	
	animation_component.setup(animation_dict)
	
	reset()
		
	health_component.life_lost.connect(func(): animation_component.play_and_monitor(ANIMATION_HEART_LOST))
	health_component.last_life_left.connect(func(): animation_component.play_and_monitor(ANIMATION_HEART_BEATING))
	health_component.died.connect(func(): animation_component.play(ANIMATION_NONE_HEARTS_LEFT))

func reset() -> void:
	health_component.reset(max_life_count)
	heart_display_component.reset(max_life_count)

func decrease_life_count() -> void:
	health_component.decrease_life_count()
	await animation_component.animation_completion(ANIMATION_HEART_LOST)	

func _start_heart_lost_animation() -> Tween:
	if skip_death_animation:
		return create_tween()
	
	var heart := heart_display_component.full_hearts[health_component.life_count]
	return Animations.scale_to_zero(heart, 0.2)

func _start_heart_beating_animation() -> Tween:
	var heart := heart_display_component.full_hearts[0]
	return Animations.beating(heart, [0.1, 0.2, 0.2, 0.2])
	
func _start_bliping_hearts_animation() -> Tween:
	animation_component.cancel_running_animation(ANIMATION_HEART_BEATING)
	
	var bliping := Animations.bliping(heart_display_component.empty_hearts, [0.2, 0.2])
	bliping.finished.connect(func(): death_animation_finished.emit(), CONNECT_ONE_SHOT)
	
	return bliping
