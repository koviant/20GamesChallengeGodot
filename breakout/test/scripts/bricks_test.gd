extends GdUnitTestSuite
#@warning_ignore('unused_parameter')
#@warning_ignore('return_value_discarded')

const __source = 'res://scripts/bricks.gd'

func test_reset_queues_for_deletion_all_old_bricks() -> void:
	# Arrange
	var grid := _create_grid()
	var runner = scene_runner("res://test/blank_scene.tscn")
	runner.scene().add_child(grid)
	grid.reset()
	var old_bricks = grid.get_children()
	
	# Act
	grid.reset()
	
	# Assert
	for b : Node in old_bricks:
		assert_bool(b.is_queued_for_deletion())

func _create_grid() -> BrickGrid:
	var grid_data := BrickGridData.new()
	grid_data.brick_height = 30
	grid_data.brick_width = 30
	grid_data.column_count = 3
	grid_data.row_count = 1
	
	var row := BrickCellRow.new()
	
	row.cells.append(BrickCell.new())
	row.cells.append(BrickCell.new())
	row.cells.append(BrickCell.new())
	grid_data.grid = [row]
	
	var grid = BrickGrid.new()
	grid.data = grid_data
	
	return grid
