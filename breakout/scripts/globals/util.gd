extends Node

enum BrickType { REGULAR, TWO_HITS, EXPLODES_ON_HIT }

func create_hit_handler(grid: BrickGrid, brick: Brick, row: int, col: int) -> HitHandlers.Base:
	match brick.type:
		BrickType.REGULAR:
			return HitHandlers.Regular.new(grid, brick)
		BrickType.TWO_HITS:
			return HitHandlers.TwoHit.new(grid, brick)
		BrickType.EXPLODES_ON_HIT:
			return HitHandlers.Explodes.new(grid, brick, row, col)
		_:
			print("Unknown brick type: " + str(brick.type))
			print("Creating regular hit handler")
			return HitHandlers.Regular.new(grid, brick)
