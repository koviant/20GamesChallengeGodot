@tool

extends Node2D

@export var data: BrickGridData

@onready var grid_container: GridContainer = %GridContainer
@onready var click_menu: PanelContainer = %ClickMenu
@onready var color_picker_button: ColorPickerButton = %ColorPickerButton
@onready var is_empty_selector: CheckButton = %IsEmptySelector

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var brick_size := Vector2(data.brick_width, data.brick_height)
	grid_container.columns = data.column_count
	grid_container.add_theme_constant_override("h_separation", data.column_spacing)
	grid_container.add_theme_constant_override("v_separation", data.row_spacing)
	for i in range(data.row_count):
		for j in range(data.column_count):
			var cell_data = data.cell_at(i, j)
			var rect = ColorRect2.new()
			rect.custom_minimum_size = brick_size
			rect.color = cell_data.color
			
			rect.right_click.connect(on_colored_rect_gui_event)
			
			grid_container.add_child(rect)

func on_colored_rect_gui_event(brick: ColorRect2, global_position: Vector2):
		click_menu.position = global_position
		click_menu.show()
		for conn in color_picker_button.color_changed.get_connections():
			color_picker_button.color_changed.disconnect(conn.callable)
			
		color_picker_button.color = brick.color
		color_picker_button.color_changed.connect(func(color): change_color(brick, color))
		is_empty_selector.toggled.connect(func(on): set_empty(brick, on))
			

func change_color(brick: ColorRect2, color: Color) -> void:
	brick.color = color

func set_empty(brick: ColorRect2, empty: bool) -> void:
	if empty:
		brick.color = Color.TRANSPARENT
		brick.border_color = Color.BLACK
	else:
		brick.color = color_picker_button.color
		brick.border_color = Color.TRANSPARENT

func _on_grid_container_gui_input(event: InputEvent) -> void:
	if event is InputEventMouseButton:
		click_menu.hide()
