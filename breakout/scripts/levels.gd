extends Node

var _levels: PackedStringArray
var _idx := 0

func _ready() -> void:
	_levels = DirAccess.get_files_at("res://levels")
	
func has_next() -> bool:
	return _idx < len(_levels)
	
func next() -> BrickGridData:
	var level := _levels[_idx]
	_idx += 1
	return ResourceLoader.load("res://levels/" + level)
