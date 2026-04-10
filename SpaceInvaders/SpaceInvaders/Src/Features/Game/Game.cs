using System;
using Godot;
using GodotUtilities;
using SpaceInvaders.Core.Features.Game;
using SpaceInvaders.Core.Features.Game.Player;
using SpaceInvaders.Extensions;
using SpaceInvaders.Features.Enemy;

namespace SpaceInvaders.Features.Game;

[Scene]
public partial class Game : Node2D, IGameView
{
    [Node] private CanvasLayer _hud;
    [Node] private Player _player;
    [Node] private Projectile _playerProjectile;
    [Node] private EnemyGrid _enemyGrid;

    public IPlayerView Player => _player;
    public IProjectileView ProjectileView => _playerProjectile;

    public override void _Ready()
    {
        WireNodes();

        _player.HeartDisplayComponent.HudLayer = _hud;
        _player.HeartDisplayComponent.HeartStartPosition = new Vector2(100, this.ViewportSize.Y - 80);

        _player.Position = new Vector2(this.ViewportSize.X / 2 - _player.Size.X / 2, this.ViewportSize.Y - 200);
        _enemyGrid.Position = new Vector2((this.ViewportSize.X - _enemyGrid.TotalWidth) / 2, 50);
        
        _enemyGrid.Reset();
        _enemyGrid.State = EnemyGridState.MovingLeft;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        ProcessPlayerProjectile(delta);
        ProcessEnemyGrid(delta);
    }

    private void ProcessEnemyGrid(double delta)
    {
        if (_enemyGrid.State is EnemyGridState.MovingLeft)
        {
            if (_enemyGrid.Position.X > 0)
            {
                _enemyGrid.Position = _enemyGrid.Position with { X = Math.Max(_enemyGrid.Position.X - 10, 0) };
            }
            else
            {
                _enemyGrid.State = EnemyGridState.MovingRight;
            }
        }
        else if (_enemyGrid.State is EnemyGridState.MovingRight)
        {
            if (_enemyGrid.Position.X + _enemyGrid.TotalWidth < this.ViewportSize.X)
            {
                _enemyGrid.Position = _enemyGrid.Position with { X = Math.Min(_enemyGrid.Position.X + 10, this.ViewportSize.X - _enemyGrid.TotalWidth) };
            }
            else
            {
                _enemyGrid.State = EnemyGridState.MovingLeft;
            }
        }
        else if (Input.IsActionPressed("ui_down"))
        {
            _enemyGrid.State = EnemyGridState.Stopped;
        }
    } 

    private void ProcessPlayerProjectile(double delta)
    {
        if (_playerProjectile.State is ProjectileState.Flying)
        {
            _playerProjectile.Position += _playerProjectile.Speed.ToGodotVector() * (float)delta;
        }
        else if (_playerProjectile.State is ProjectileState.Ready)
        {
            _playerProjectile.Position = new Vector2(
                _player.Position.X + _player.Size.X / 2 - _playerProjectile.Size.X / 2,
                _player.Position.Y - _playerProjectile.Size.Y);
        }
    }
}