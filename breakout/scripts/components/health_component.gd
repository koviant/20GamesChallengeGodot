class_name HealthComponent extends Node

signal life_lost
signal last_life_left
signal died

var _life_count: int

var alive: bool: 
	get: return _life_count > 0

var life_count: int:
	get: return _life_count

func reset(max_life_count: int) -> void:
	_life_count = max_life_count

func decrease_life_count():
	assert(_life_count >= 0, "calling decrease_life_count on invalid life count")
	
	_life_count -= 1
	
	life_lost.emit()
	
	if _life_count == 1:
		last_life_left.emit()
	
	if _life_count == 0:
		died.emit()
