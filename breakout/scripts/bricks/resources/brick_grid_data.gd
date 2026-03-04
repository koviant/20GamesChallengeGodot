class_name BrickGridData extends Resource

@export var grid: Array[BrickCellRow]
@export var row_count: int
@export var column_count: int
@export var brick_width: int = 60
@export var brick_height: int = 30
@export var column_spacing: int = 10
@export var row_spacing: int = 10

func resize_if_needed() -> void:
	if len(grid) != row_count:
		grid.resize(row_count)
	
	for i in range(len(grid)):
		if not grid[i]:
			grid[i] = _get_empty_row(column_count)
		elif len(grid[i].cells) < column_count:
			var old_size := len(grid[i].cells)
			grid[i].cells.resize(column_count)
			for j in range(old_size, column_count):
				grid[i].cells[j] = _get_empty_cell()
		elif len(grid[i].cells) > column_count:
			grid[i].cells.resize(column_count)

func cell_at(row: int, column: int) -> BrickCell:
	assert(row >= 0 && row < row_count, "row out of bounds")
	assert(column >= 0 && column < column_count, "column out of bounds")
	
	if not grid[row]:
		grid[row] = _get_empty_row(column_count)
			
	return grid[row].cells[column]

func _get_empty_row(length: int) -> BrickCellRow:
	var new_row = BrickCellRow.new()
	new_row.cells.resize(length)
	for j in range(len(new_row.cells)):
		new_row.cells[j] = _get_empty_cell()
			
	return new_row

func _get_empty_cell() -> BrickCell:
	var b := BrickCell.new()
	b.is_empty = true
	b.color = Color.TRANSPARENT
	
	return b
