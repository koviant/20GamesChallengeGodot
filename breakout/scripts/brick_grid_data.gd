class_name BrickGridData extends Resource

@export var grid: Array[BrickCell]
@export var row_count: int
@export var column_count: int
@export var brick_width: int = 60
@export var brick_height: int = 30
@export var column_spacing: int = 10
@export var row_spacing: int = 10

#@warning_ignore("shadowed_variable")
#func _init(
	#grid: Array[BrickCell] = [], 
	#row_count: int = 0,
	#column_count: int = 0,
	#brick_width: int = 60,
	#brick_height: int = 30,
	#column_spacing: int = 10,
	#row_spacing: int = 10) -> void:
		#self.grid = grid
		#self.row_count = row_count
		#self.column_count = column_count
		#self.brick_width = brick_width
		#self.brick_height = brick_height
		#self.column_spacing = column_spacing
		#self.row_spacing = row_spacing
		#
		#assert(len(grid) == row_count * column_count)

func cell_at(row: int, column: int) -> BrickCell:
	assert(row >= 0 && row < row_count, "row out of bounds")
	assert(column >= 0 && column < column_count, "column out of bounds")
	
	return grid[row*column_count + column]
