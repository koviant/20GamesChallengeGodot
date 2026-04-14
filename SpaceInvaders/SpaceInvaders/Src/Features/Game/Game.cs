using Godot;
using GodotUtilities;
using SpaceInvaders.Core.Features.Enemy.Interfaces;
using SpaceInvaders.Core.Features.Game;
using SpaceInvaders.Core.Features.Game.Player;
using SpaceInvaders.Extensions;
using SpaceInvaders.Features.Enemy;
using SpaceInvaders.Features.Player;
using SpaceInvaders.StartableNodes;

namespace SpaceInvaders.Features.Game;

[Scene]
public partial class Game : StartableNode2D, IGameView
{
    [Node] private EnemyGrid _enemyGrid;
    [Node] private CanvasLayer _hud;
    [Node] private Player.Player _player;
    [Node] private Projectile _playerProjectile;
    [Node] private Marker2D _projectileResetMarker;

    public IPlayerView Player => _player;
    public IProjectileView ProjectileView => _playerProjectile;
    public IEnemyGridView EnemyGridView => _enemyGrid;

    public override void _Ready()
    {
        WireNodes();
    }

    public override void ViewStart()
    {
        _player.HeartDisplayComponent.HudLayer = _hud;
        _player.HeartDisplayComponent.HeartStartPosition = new Vector2(100, this.ViewportSize.Y - 80);

        _player.Position = new Vector2(this.ViewportSize.X / 2 - _player.Size.X / 2, this.ViewportSize.Y - 200);
        _enemyGrid.Position = new Vector2((this.ViewportSize.X - _enemyGrid.TotalWidth) / 2, 50);

        _playerProjectile.ResetMarker = _projectileResetMarker;
    }

    public override void _PhysicsProcess(double delta)
    {
        _projectileResetMarker.Position = new Vector2(
            _player.Position.X + _player.Size.X / 2 - _playerProjectile.Size.X / 2,
            _player.Position.Y - _playerProjectile.Size.Y);
    }
}