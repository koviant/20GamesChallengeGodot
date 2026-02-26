class_name ColorRect2 extends ColorRect

signal mouse_click(caller: ColorRect2, global_position: Vector2)

var cell_color: Color:
	get: return data.color
	set(value):
		data.color = value
		color = value

var empty: bool:
	get: return data.is_empty
	set(value):
		data.is_empty = value
		queue_redraw()

var data: BrickCell:
	get: return data
	set(value):
		data = value
		color = data.color if not data.is_empty else Color.TRANSPARENT
		queue_redraw()

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
