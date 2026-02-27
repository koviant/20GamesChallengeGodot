@tool
extends Node2D

@export var data: BrickGridData

@onready var grid_container: GridContainer = %GridContainer
@onready var click_menu: PanelContainer = %ClickMenu
@onready var color_picker_button: ColorPickerButton = %ColorPickerButton
@onready var is_empty_selector: CheckButton = %IsEmptySelector
@onready var copy_button: Button = %CopyButton
@onready var paste_button: Button = %PasteButton
@onready var copy_to_row_button: Button = %CopyToRowButton

var clipboard: BrickCell

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	reload()
	
# TODO UPDATE GRID DATA ON SAVE, OTHERWISE CELLS THAT WERE NOT IN THE DATA INITIALLY WILL NOT BE SAVED
	
func reload() -> void:
	data.resize_if_needed()
	for ch in grid_container.get_children():
		grid_container.remove_child(ch)
	
	var brick_size := Vector2(data.brick_width, data.brick_height)
	grid_container.columns = data.column_count
	grid_container.add_theme_constant_override("h_separation", data.column_spacing)
	grid_container.add_theme_constant_override("v_separation", data.row_spacing)
	for row in range(data.row_count):
		for column in range(data.column_count):
			var cell_data = data.cell_at(row, column)
			var cell = BrickRect.new(cell_data, row, column)
			cell.custom_minimum_size = brick_size
			cell.mouse_click.connect(_on_cell_clicked)
			grid_container.add_child(cell)

func _on_cell_clicked(brick: BrickRect, click_position: Vector2):
		click_menu.position = click_position
		click_menu.hide()
		click_menu.show()
		
		color_picker_button.color = brick.color
		is_empty_selector.set_pressed_no_signal(brick.empty)
		
		if clipboard:
			paste_button.show()
		
		var color_changed_callable = func(color): change_color(brick, color)
		color_picker_button.color_changed.connect(color_changed_callable)
		
		var empty_changed_callable = func(on): set_empty(brick, on)
		is_empty_selector.toggled.connect(empty_changed_callable)	
		
		var copy_cell_callable = func(): clipboard = brick.data
			
		copy_button.pressed.connect(copy_cell_callable)
		
		var paste_cell_callable = func(): 
			_copy(clipboard, brick)
			click_menu.hide()
			
		paste_button.pressed.connect(paste_cell_callable)
		
		var copy_to_row_callable = func():
			for col in range(grid_container.columns):
				var idx = grid_container.columns * brick.row + col
				var rect: BrickRect = grid_container.get_child(idx)
				_copy(brick.data, rect)
		
		copy_to_row_button.pressed.connect(copy_to_row_callable)
		
		var disconnect_callable = func():
			color_picker_button.color_changed.disconnect(color_changed_callable)
			is_empty_selector.toggled.disconnect(empty_changed_callable)	
			copy_button.pressed.disconnect(copy_cell_callable)	
			paste_button.pressed.disconnect(paste_cell_callable)
			copy_to_row_button.pressed.disconnect(copy_to_row_callable)
			
		click_menu.hidden.connect(disconnect_callable, ConnectFlags.CONNECT_ONE_SHOT)

func _copy(from_data: BrickCell, to_rect: BrickRect) -> void:
	to_rect.cell_color = from_data.color
	to_rect.empty = from_data.is_empty

func change_color(brick: BrickRect, color: Color) -> void:
	if not brick.empty:
		brick.cell_color = color

func set_empty(brick: BrickRect, empty: bool) -> void:
	brick.empty = empty	
	brick.color = Color.TRANSPARENT if empty else color_picker_button.color

func _save_button_pressed() -> void:
	ResourceSaver.save(data, "res://levels/01.tres")

func _on_grid_container_gui_input(event: InputEvent) -> void:
	if event is InputEventMouseButton:
		click_menu.hide()

func _reload_button_pressed() -> void:
	data = ResourceLoader.load("res://levels/01.tres")
	reload()
