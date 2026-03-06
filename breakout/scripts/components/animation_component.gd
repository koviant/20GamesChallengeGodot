class_name AnimationComponent extends Node

## Dictionary[animation_key, tween_factory]
var _animations: Dictionary[Animations.Key, Callable]

func setup(animations: Dictionary[Animations.Key, Callable]) -> void:
	_animations = animations

func play(key: Animations.Key) -> void:
	var callable := _animations[key] 
	assert(callable)
	
	var tween: Tween = callable.call()
	Animations.monitor(key, tween)
