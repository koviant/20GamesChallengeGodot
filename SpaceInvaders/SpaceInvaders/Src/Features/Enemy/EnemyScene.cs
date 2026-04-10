using Godot;
using GodotUtilities;
using SpaceInvaders.Core.Features.Enemy;

namespace SpaceInvaders.Features.Enemy;

[Scene]
[GlobalClass]
public partial class EnemyScene : Area2D
{
	[Node] private CollisionShape2D _collisionShape2D;
	[Node] private MeshInstance2D _meshInstance2D;
	
	private static PackedScene _scene = GD.Load<PackedScene>("res://Src/Features/Enemy/enemy.tscn");

	public Vector2 Size { get; set; }
	public Color MeshColor { get; set; }
	
	public static EnemyScene Create(Vector2 size, EnemyResource data)
	{
		var enemy = _scene.Instantiate<EnemyScene>();

		enemy.Size = size;
		enemy.MeshColor = data.Type is EnemyType.Default ? Colors.Blue : Colors.Red;

		return enemy;
	}

	public override void _Ready()
	{
		WireNodes();
		
		_collisionShape2D.Shape = new RectangleShape2D
		{
			Size = Size,
		};

		_collisionShape2D.Position = Size / 2;
		
		var mesh = new QuadMesh
		{
			Size = Size,
		};
		
		_meshInstance2D.Modulate = MeshColor;
		_meshInstance2D.Mesh = mesh;
		_meshInstance2D.Position = Size / 2;
	}
}