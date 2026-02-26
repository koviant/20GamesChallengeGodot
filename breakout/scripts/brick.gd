class_name Brick extends RigidBody2D

const scene: PackedScene = preload("res://scenes/brick.tscn")
const pixels_per_meter := 16

var _mesh_color: Color

static func create(size: Vector2, color: Color) -> Brick:
	var brick: Brick = scene.instantiate()

	brick.set_size(size)
	
	brick._mesh_color = color
	
	return brick

func _ready() -> void:
	$MeshInstance2D.modulate = _mesh_color

func set_size(value: Vector2) -> void:
	var shape = RectangleShape2D.new()
	shape.size = value
	$CollisionShape2D.shape = shape
	
	var mesh = QuadMesh.new()
	mesh.size = _to_mesh_size_meters(value)
	$MeshInstance2D.mesh = mesh

func _to_mesh_size_meters(value: Vector2) -> Vector2:
	return value / pixels_per_meter
