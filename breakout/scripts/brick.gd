class_name Brick extends RigidBody2D

const scene: PackedScene = preload("res://scenes/brick.tscn")
const pixels_per_meter := 16

static func create(size: Vector2) -> Brick:
	var brick: Brick = scene.instantiate()

	brick.set_size(size)
	
	return brick

func set_size(value: Vector2) -> void:
	var shape = RectangleShape2D.new()
	shape.size = value
	$CollisionShape2D.shape = shape
	
	var mesh = QuadMesh.new()
	mesh.size = _to_mesh_size_meters(value)
	$MeshInstance2D.mesh = mesh

func _to_mesh_size_meters(value: Vector2) -> Vector2:
	return value / pixels_per_meter
