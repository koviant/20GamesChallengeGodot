extends Node

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
