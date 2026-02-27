class_name BrickRect extends ColorRect

signal mouse_click(caller: BrickRect, global_position: Vector2)

var cell_color: Color:
	get: return _data.color
	set(value):
		_data.color = value
		color = value

var empty: bool:
	get: return _data.is_empty
	set(value):
		_data.is_empty = value
		queue_redraw()

var data: BrickCell:
	get: return _data

var _data: BrickCell
var row: int
var column: int

func _init(data: BrickCell, row: int, column: int) -> void:
	_data = data
	color = _data.color if not _data.is_empty else Color.TRANSPARENT
	self.row = row
	self.column = column

var border_color: Color = Color.BLACK

func _ready() -> void:
	self.gui_input.connect(_on_gui_event)
	
func _draw() -> void:
	if empty:
		draw_rect(Rect2(Vector2(-2, -2), Vector2(size.x+4, size.y+4)), border_color, false, 2)
	
func _on_gui_event(event: InputEvent) -> void:
	if event is InputEventMouseButton:
		var mb = event as InputEventMouseButton
		if mb.button_index in [MOUSE_BUTTON_LEFT, MOUSE_BUTTON_RIGHT] and mb.is_released():
			mouse_click.emit(self, event.global_position)
