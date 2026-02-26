class_name ColorRect2 extends ColorRect

signal right_click(caller: ColorRect2, global_position: Vector2)

@export var border_color: Color = Color.TRANSPARENT:
	set(value):
		border_color = value
		queue_redraw()

func _ready() -> void:
	self.gui_input.connect(_on_gui_event)
	
func _draw() -> void:
	draw_rect(Rect2(Vector2(-2, -2), Vector2(size.x+4, size.y+4)), border_color, false, 2)
	
func _on_gui_event(event: InputEvent) -> void:
	if event is InputEventMouseButton:
		var mb = event as InputEventMouseButton
		if mb.button_index == MouseButton.MOUSE_BUTTON_RIGHT and mb.is_released():
			right_click.emit(self, event.global_position)
