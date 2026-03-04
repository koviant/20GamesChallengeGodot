class_name HitHandlers

@abstract class Base:
	var _grid: BrickGrid
	var _brick: Brick

	func _init(grid: BrickGrid, brick: Brick):
		_grid = grid
		_brick = brick	

	@abstract func handle() -> void

	func destroy_brick() -> void:
		_grid.remove_brick(_brick)

class Regular extends Base:
	func _init(grid: BrickGrid, brick: Brick):
		super(grid, brick)
		
	func handle() -> void: 
		destroy_brick()

class TwoHit extends Base:
	var _hits := 0
	const CRACKS: Texture2D = preload("uid://br54rptpmf8o5")

	func _init(grid: BrickGrid, brick: Brick):
		super(grid, brick)

	func handle() -> void:
		_hits += 1
		if _hits == 1:
			_brick.sprite.texture = CRACKS
			var spriteScaleX = _brick.size.x / CRACKS.get_width()
			var spriteScaleY =  _brick.size.y / CRACKS.get_height()
			_brick.sprite.scale = Vector2(spriteScaleX, spriteScaleY)
		if _hits == 2:
			destroy_brick()

class Explodes extends Base:
	var _row: int
	var _col: int
	
	const EXPLODING_BRICK: Texture2D = preload("uid://ciiuawpbqf7b")
	
	func _init(grid: BrickGrid, brick: Brick, row: int, col: int):
		super(grid, brick)
		_row = row
		_col = col
		
		_brick.texture = EXPLODING_BRICK
		var spriteScaleY =  _brick.size.y / EXPLODING_BRICK.get_height()
		_brick.sprite_scale = Vector2(spriteScaleY, spriteScaleY)
		
	func handle() -> void: 
		var initial_modulate := _brick.mesh_instance_2d.modulate
		var tween = _brick.create_tween()
		tween.tween_property(_brick.mesh_instance_2d, "modulate", Color.WHITE, 0.2)
		tween.tween_property(_brick.mesh_instance_2d, "modulate", initial_modulate, 0.2)
		tween.tween_property(_brick.mesh_instance_2d, "modulate", Color.WHITE, 0.2)
		tween.tween_property(_brick.mesh_instance_2d, "modulate", initial_modulate, 0.2)
		tween.tween_callback(_remove_neighbours)
		
	func _remove_neighbours():
		_hit_if_present(_row - 1, _col)
		_hit_if_present(_row + 1, _col)
		_hit_if_present(_row - 1, _col - 1)
		_hit_if_present(_row + 1, _col - 1)
		_hit_if_present(_row - 1, _col + 1)
		_hit_if_present(_row + 1, _col + 1)
		_hit_if_present(_row, _col + 1)
		_hit_if_present(_row, _col - 1)
		_grid.remove_brick(_brick)

	func _hit_if_present(row: int, col: int) -> void:
		var b = _grid.get_at(row, col)
		if b:
			b.hit()
