using System;
using Godot;
using GodotUtilities;
using SpaceInvaders.Core.Features.Game;
using SpaceInvaders.Extensions;
using Color = System.Drawing.Color;

namespace SpaceInvaders.Scripts.Scenes;

[Scene]
public partial class Projectile : Area2D, IProjectileView
{
    [Node] private MeshInstance2D _meshInstance;
    [Node] private CollisionShape2D _collisionShape2D;

    public Vector2 Size => _collisionShape2D.Shape.GetRect().Size;

    public override void _Ready()
    {
        WireNodes();

        AreaEntered += obj =>
        {
            obj.QueueFree();
            Hit?.Invoke();
        };

        BodyShapeEntered += (_, obj, _, _) =>
        {
            obj.QueueFree();
            Hit?.Invoke();
        };
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Position.X < 0 || Position.X >= this.ViewportSize.X ||
            Position.Y < 0 || Position.Y >= this.ViewportSize.Y)
        {
            OutOfBounds?.Invoke();
        }
    }

    public event Action? Hit;
    public event Action? OutOfBounds;

    public Color Color
    {
        get => _meshInstance.Modulate.ToNativeColor();
        set => _meshInstance.Modulate = value.ToGodotColor();
    }

    public System.Numerics.Vector2 Speed { get; set; }
    public ProjectileState State { get; set; }
}