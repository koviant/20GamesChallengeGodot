using System;
using System.Diagnostics;
using Godot;
using GodotUtilities;
using SpaceInvaders.Core.Features.Player;
using SpaceInvaders.Extensions;
using SpaceInvaders.Framework.Events;
using SpaceInvaders.Framework.Utils;
using Color = System.Drawing.Color;
using StartableArea2D = SpaceInvaders.StartableNodes.StartableArea2D;

namespace SpaceInvaders.Features.Player;

[Scene]
public partial class Projectile : StartableArea2D, IProjectileView
{
    [Node] private CollisionShape2D _collisionShape2D;
    [Node] private MeshInstance2D _meshInstance;

    private InvokableEvent<IView> _hit = new();
    private InvokableEvent _outOfBounds = new();

    public Vector2 Size => _collisionShape2D.Shape.GetRect().Size;
    public Marker2D? ResetMarker { get; set; }

    public Event<IView> Hit => _hit;
    public Event<Unit> OutOfBounds => _outOfBounds;

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
                _outOfBounds.Invoke();
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
            _hit.Invoke(view);
        }
    }
}