class_name ClickMenu extends PanelContainer

signal paste_row_clicked(row: int, brick: BrickCell)

@onready var is_empty_selector: CheckButton = %IsEmptySelector
@onready var label: Label = $VBoxContainer/HBoxContainer/Label
@onready var color_picker_button: ColorPickerButton = %ColorPickerButton
@onready var copy_button: Button = %CopyButton
@onready var paste_button: Button = %PasteButton
@onready var copy_to_row_button: Button = %CopyToRowButton

func show_menu(brick: BrickRect, click_position: Vector2):
	if click_position.x + size.x > get_viewport_rect().size.x:
		position = Vector2(click_position.x - size.x, click_position.y)
	else:
		position = click_position
	
	hide()
	show()
	
	color_picker_button.color = brick.color
	is_empty_selector.set_pressed_no_signal(brick.empty)
	
	if Clipboard.value:
		paste_button.show()
	
	var color_changed_callable = func(color): change_color(brick, color)
	color_picker_button.color_changed.connect(color_changed_callable)
	
	var empty_changed_callable = func(on): set_empty(brick, on)
	is_empty_selector.toggled.connect(empty_changed_callable)	
	
	var copy_cell_callable = func(): Clipboard.value = brick.data
		
	copy_button.pressed.connect(copy_cell_callable)
	
	var paste_cell_callable = func(): 
		brick.copy_data_from(Clipboard.value)
		hide()
		
	paste_button.pressed.connect(paste_cell_callable)
	
	var copy_to_row_callable = func(): paste_row_clicked.emit(brick.row, brick.data)
	copy_to_row_button.pressed.connect(copy_to_row_callable)
	
	await hidden
	
	color_picker_button.color_changed.disconnect(color_changed_callable)
	is_empty_selector.toggled.disconnect(empty_changed_callable)	
	copy_button.pressed.disconnect(copy_cell_callable)	
	paste_button.pressed.disconnect(paste_cell_callable)
	copy_to_row_button.pressed.disconnect(copy_to_row_callable)

func change_color(brick: BrickRect, color: Color) -> void:
	is_empty_selector.set_pressed_no_signal(false)
	brick.empty = false
	brick.cell_color = color

func set_empty(brick: BrickRect, empty: bool) -> void:
	brick.empty = empty	
	brick.color = Color.TRANSPARENT if empty else color_picker_button.color
