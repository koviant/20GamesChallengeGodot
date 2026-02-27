class_name BrickGridData extends Resource

@export var grid: Array[BrickCell]
@export var row_count: int
@export var column_count: int
@export var brick_width: int = 60
@export var brick_height: int = 30
@export var column_spacing: int = 10
@export var row_spacing: int = 10

func resize_if_needed() -> void:
	if len(grid) != row_count * column_count:
		grid.resize(row_count * column_count)

func cell_at(row: int, column: int) -> BrickCell:
	assert(row >= 0 && row < row_count, "row out of bounds")
	assert(column >= 0 && column < column_count, "column out of bounds")
	
	var idx = row*column_count + column
	if not grid[idx]:
		var b = BrickCell.new()
		b.is_empty = true
		b.color = Color.TRANSPARENT
		grid[idx] = b
	
	return grid[idx]
