@tool
extends Node2D

@export var data: BrickGridData

@onready var grid_container: GridContainer = %GridContainer
@onready var click_menu: ClickMenu = %ClickMenu
@onready var color_picker_button: ColorPickerButton = %ColorPickerButton
@onready var is_empty_selector: CheckButton = %IsEmptySelector
@onready var copy_button: Button = %CopyButton
@onready var paste_button: Button = %PasteButton
@onready var copy_to_row_button: Button = %CopyToRowButton
@onready var rows_input: TextEdit = %RowsTextEdit
@onready var columns_input: TextEdit = %ColumnsTextEdit
@onready var level_selector: OptionButton = %LevelSelector

const level_folder := "res://levels/"

func _ready() -> void:
	reload()

func reload() -> void:
	_prepare_ui()
	
	data.resize_if_needed()
		
	var brick_size := Vector2(data.brick_width, data.brick_height)
	grid_container.columns = data.column_count
	grid_container.add_theme_constant_override("h_separation", data.column_spacing)
	grid_container.add_theme_constant_override("v_separation", data.row_spacing)
	for row in range(data.row_count):
		for column in range(data.column_count):
			var cell_data = data.cell_at(row, column)
			var cell = BrickRect.new(cell_data, row, column)
			cell.custom_minimum_size = brick_size
			cell.mouse_click.connect(click_menu.show_menu)
			grid_container.add_child(cell)

func _prepare_ui():
	var dir := DirAccess.open(level_folder)
	assert(dir != null, "folder could not be opened")
	level_selector.get_popup().add_theme_font_size_override("font_size", 32)
	
	if level_selector.item_count == 0:
		for file: String in dir.get_files():
			level_selector.add_item(level_folder + file)
	
	rows_input.text = str(data.row_count)
	columns_input.text = str(data.column_count)
	click_menu.paste_row_clicked.connect(_paint_row)
	
	for ch in grid_container.get_children():
		grid_container.remove_child(ch)

func _paint_row(row: int, brick: BrickCell) -> void:
	for col in range(grid_container.columns):
		var idx = grid_container.columns * row + col
		var rect: BrickRect = grid_container.get_child(idx)
		rect.copy_data_from(brick)

func _save_button_pressed() -> void:
	ResourceSaver.save(data, level_selector.text)

func _reload_button_pressed() -> void:
	_load_data_and_reload_tool(level_selector.text)

func _update_pressed() -> void:
	if not rows_input.text.is_valid_int() or not columns_input.text.is_valid_int():
		return
		
	var rows := rows_input.text.to_int()
	var column := columns_input.text.to_int()
	
	data.row_count = rows
	data.column_count = column
	data.resize_if_needed()
	reload()

func _on_background_input(event: InputEvent) -> void:
	if event is InputEventMouseButton:
		click_menu.hide()

func _on_level_selected(index: int) -> void:
	var level_path := level_selector.get_item_text(index)
	_load_data_and_reload_tool(level_path)

func _load_data_and_reload_tool(path: String) -> void:
	data = ResourceLoader.load(path, "", ResourceLoader.CACHE_MODE_IGNORE)
	reload()
	
