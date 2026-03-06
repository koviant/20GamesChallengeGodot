class_name AnimationComponent extends Node

## Dictionary[animation_key, tween_factory]
var _animations: Dictionary[String, Callable]

var _running_animations: Dictionary[String, WeakRefTween]

func setup(animations: Dictionary[String, Callable]) -> void:
	_animations = animations

func play_and_monitor(key: String) -> void:
	var tween = _create_tween(key)
	_monitor(key, tween)
	
func play(key: String) -> void:
	_create_tween(key)

func animation_completion(key: String) -> void:
	_assert_valid_key(key)
	var weak_reference: WeakRefTween = _running_animations.get(key)
	assert(weak_reference, "This animation is not being monitored")
	
	var tween: Tween = weak_reference.get_tween_or_null()
	if tween and tween.is_running():
		await tween.finished

func cancel_running_animation(key: String) -> void:
	_assert_valid_key(key)
	var weak_reference: WeakRefTween = _running_animations.get(key)
	if not weak_reference:
		return
	
	var tween: Tween = weak_reference.get_tween_or_null()
	if tween and tween.is_running():
		tween.kill()

func _monitor(key: String, tween: Tween) -> void:
	_assert_valid_key(key)
	_running_animations[key] = WeakRefTween.new(tween)

func _create_tween(key: String) -> Tween:
	_assert_valid_key(key)
	var tween = _animations.get(key).call()
	assert(tween is Tween)
	return tween
	
func _assert_valid_key(key: String) -> void:
	assert(key in _animations.keys(), "Provided key " + key + " is not defined")
