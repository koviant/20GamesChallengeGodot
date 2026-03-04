class_name Brick extends RigidBody2D

const scene: PackedScene = preload("res://scenes/brick.tscn")
const pixels_per_meter := 16

var type: Util.BrickType

@onready var sprite: Sprite2D = %Sprite

var _mesh_color: Color
var size: Vector2:
	get: return size

var hit_handler: HitHandlers.Base
var texture: Texture2D
var sprite_scale: Vector2

@onready var mesh_instance_2d: MeshInstance2D = %MeshInstance2D
@onready var collision_shape_2d: CollisionShape2D = %CollisionShape2D

static func create(brick_size: Vector2, data: BrickCell) -> Brick:
	var brick: Brick = scene.instantiate()

	brick.size = brick_size
	brick.type = data.type
	brick._mesh_color = data.color
	
	return brick

func _ready() -> void:
	mesh_instance_2d.modulate = _mesh_color
	if texture:
		sprite.texture = texture
	
	if sprite_scale:
		sprite.scale = sprite_scale
	
	_set_child_size()

func hit() -> void:
	hit_handler.handle()

func _set_child_size() -> void:
	var shape = RectangleShape2D.new()
	shape.size = size
	collision_shape_2d.shape = shape
	
	var mesh = QuadMesh.new()
	mesh.size = _to_mesh_size_meters(size)
	mesh_instance_2d.mesh = mesh

func _to_mesh_size_meters(value: Vector2) -> Vector2:
	return value / pixels_per_meter
