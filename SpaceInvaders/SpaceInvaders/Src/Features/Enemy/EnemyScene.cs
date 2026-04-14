using Godot;
using GodotUtilities;
using SpaceInvaders.Core.Features.Enemy;
using SpaceInvaders.Extensions;
using StartableArea2D = SpaceInvaders.StartableNodes.StartableArea2D;

namespace SpaceInvaders.Features.Enemy;

[Scene]
[GlobalClass]
public partial class EnemyScene : StartableArea2D, IEnemyView
{
    private static readonly PackedScene Scene = GD.Load<PackedScene>("res://Src/Features/Enemy/enemy.tscn");
    [Node] private CollisionShape2D _collisionShape2D;
    [Node] private MeshInstance2D _meshInstance2D;

    public Vector2 Size { get; set; }
    public Color MeshColor { get; set; }
    public System.Drawing.Color Color { get; set; }

    public void PlaceAt(System.Numerics.Vector2 position)
    {
        Position = position.ToGodotVector();
    }

    public void Die()
    {
        QueueFree();
    }

    public static EnemyScene Create(System.Numerics.Vector2 size, IEnemyData data)
    {
        var enemy = Scene.Instantiate<EnemyScene>();

        enemy.Size = size.ToGodotVector();
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