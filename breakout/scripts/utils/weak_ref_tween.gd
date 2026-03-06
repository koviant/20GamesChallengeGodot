class_name WeakRefTween

var _tween_ref: WeakRef

func _init(tween: Tween) -> void:
	_tween_ref = weakref(tween)

func get_tween_or_null() -> Tween:
	return _tween_ref.get_ref()
