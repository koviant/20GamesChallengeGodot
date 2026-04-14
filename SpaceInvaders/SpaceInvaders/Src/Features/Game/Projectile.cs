using System;
using System.Diagnostics;
using Godot;
using GodotUtilities;
using SpaceInvaders.Core.Features.Game.Player;
using SpaceInvaders.Core.Utils;
using SpaceInvaders.Extensions;
using Color = System.Drawing.Color;
using StartableArea2D = SpaceInvaders.StartableNodes.StartableArea2D;

namespace SpaceInvaders.Features.Game;

[Scene]
public partial class Projectile : StartableArea2D, IProjectileView
{
    [Node] private CollisionShape2D _collisionShape2D;
    [Node] private MeshInstance2D _meshInstance;

    public Vector2 Size => _collisionShape2D.Shape.GetRect().Size;
    public Marker2D? ResetMarker { get; set; }

    public event Action<IView>? Hit;
    public event Action? OutOfBounds;

    public Color Color
    {
        get => _meshInstance.Modulate.ToNativeColor();
        set => _meshInstance.Modulate = value.ToGodotColor();
    }

    public System.Numerics.Vector2 Speed { get; set; }
    public ProjectileState State { get; set; }

    public override void _Ready()
    {
        WireNodes();

        AreaEntered += OnHit;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (State is ProjectileState.Flying)
        {
            Position += Speed.ToGodotVector() * (float)delta;

            if (Position.X < 0 || Position.X >= this.ViewportSize.X ||
                Position.Y < 0 || Position.Y >= this.ViewportSize.Y)
            {
                OutOfBounds?.Invoke();
            }
        }
        else if (State is ProjectileState.Ready)
        {
            Debug.Assert(ResetMarker is not null);
            Position = ResetMarker.Position;
        }
    }

    private void OnHit(Area2D area)
    {
        if (area is IView view)
        {
            Hit?.Invoke(view);
        }
    }
}