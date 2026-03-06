extends Node

enum Key {
	HEART_LOST,
	HEART_BEATING,
	NONE_HEARTS_LEFT,
}

var _running: Dictionary[Animations.Key, WeakRef]

func monitor(key: Animations.Key, tween: Tween) -> void:
	_running[key] = weakref(tween)

func animation_completion(key: Animations.Key) -> void:
	var weak_reference: WeakRef = _running.get(key)
	if not weak_reference:
		return
	
	var tween: Tween = weak_reference.get_ref()
	if tween and tween.is_running():
		await tween.finished

func cancel_running_animation(key: Animations.Key) -> void:
	var weak_reference := _running[key]
	if not weak_reference:
		return
	
	var tween: Tween = weak_reference.get_ref()
	if tween and tween.is_running():
		tween.kill()

func scale_to_zero(sprite: Sprite2D, duration: float) -> Tween:
	var tween = create_tween()
	tween.tween_property(sprite, "scale", Vector2.ZERO, duration)
	
	return tween

func beating(sprite: Sprite2D, pattern: Array[float]) -> Tween:
	assert(len(pattern) == 4, "pattern should contain exactly 4 items")
	
	var tween = create_tween().set_loops()
	tween.tween_interval(pattern[0])
	tween.tween_property(sprite, "scale", Vector2.ONE * 0.7, pattern[1])
	tween.tween_interval(pattern[2])
	tween.tween_property(sprite, "scale", Vector2.ONE, pattern[3])
	
	return tween

func bliping(sprites: Array[Sprite2D], pattern: Array[float]) -> Tween:
	assert(len(pattern) == 2, "pattern should contain exactly 2 items")
	
	var hide_all = func () -> void:
		for s in sprites: s.hide()

	var show_all = func () -> void:
		for s in sprites: s.show()

	var tween = create_tween().set_loops(5)
	tween.tween_callback(hide_all).set_delay(pattern[0])
	tween.tween_callback(show_all).set_delay(pattern[1])
	
	return tween
