using System;
using System.Collections.Generic;
using System.Diagnostics;
using Godot;
using GodotUtilities;
using SpaceInvaders.Core.Features.Enemy;
using SpaceInvaders.Core.Features.Enemy.Interfaces;
using SpaceInvaders.Extensions;
using SpaceInvaders.StartableNodes;

namespace SpaceInvaders.Features.Enemy;

[Scene]
public partial class EnemyGrid : StartableNode2D, IEnemyGridView
{
    private int _enemyCount;

    [Export]
    public EnemyGridResource? ResourceData
    {
        get => Data as EnemyGridResource;
        set => Data = value;
    }

    public float TotalWidth
    {
        get
        {
            Debug.Assert(Data is not null);
            return Data.ColumnCount * Data.CellWidth + (Data.ColumnCount - 1) * Data.HSpacing;
        }
    }

    public event Action? HitBorder;

    public EnemyGridState State { get; set; }
    public int HSpeed { get; set; }
    public IEnemyGridData<IEnemyData>? Data { get; set; }

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
        foreach (var child in GetChildren())
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
            HitBorder?.Invoke();
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