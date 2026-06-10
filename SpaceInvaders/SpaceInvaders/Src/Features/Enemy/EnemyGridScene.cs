using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using GodotUtilities;
using SpaceInvaders.Core.Features.Enemy;
using SpaceInvaders.Core.Features.Enemy.Interfaces;
using SpaceInvaders.Extensions;
using SpaceInvaders.Framework.Events;
using SpaceInvaders.Framework.Utils;
using SpaceInvaders.StartableNodes;

namespace SpaceInvaders.Features.Enemy;

[Scene]
public partial class EnemyGridScene : StartableNode2D, IEnemyGridView
{
    private int _enemyCount;
    private readonly InvokableEvent _hitBorder = new();
    private EnemyScene? _flyingEnemy;
    
    [Export]
    public EnemyGridResource? ResourceData { get; set; }

    public float TotalWidth { get; set; }

    public Event<Unit> HitBorder => _hitBorder;

    public EnemyGridState State { get; set; }
    public int HSpeed { get; set; }

    public void SetEnemies(List<IEnemyView> enemies)
    {
        foreach (var enemyView in enemies)
        {
            if (enemyView is Node n)
            {
                AddChild(n);
            }
        }
    }

    public void ClearGrid()
    {
        foreach (var child in GetChildren().OfType<EnemyScene>())
        {
            child.QueueFree();
        }
    }

    public override void _Ready()
    {
        WireNodes();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Position.X <= 0 || Position.X >= this.ViewportSize.X - TotalWidth)
        {
            _hitBorder.Invoke();
        }

        var positionDelta = HSpeed * (float)delta;
        if (State is EnemyGridState.MovingLeft)
        {
            Position = Position.WithX(Math.Max(Position.X - positionDelta, 0f));
        }
        else if (State is EnemyGridState.MovingRight)
        {
            Position = Position.WithX(Math.Min(Position.X + positionDelta, this.ViewportSize.X - TotalWidth));
        }
    }
}