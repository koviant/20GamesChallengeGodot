using Godot;
using GodotUtilities;
using SpaceInvaders.Core.Features.Enemy.Interfaces;
using SpaceInvaders.Core.Features.Game;
using SpaceInvaders.Core.Features.Player;
using SpaceInvaders.Extensions;
using SpaceInvaders.Features.Enemy;
using SpaceInvaders.Features.Player;
using SpaceInvaders.StartableNodes;

namespace SpaceInvaders.Features.Game;

[Scene]
public partial class GameScene : StartableNode2D, IGameView
{
    [Node] private EnemyGridScene _enemyGrid;
    [Node] private CanvasLayer _hud;
    [Node] private PlayerScene _player;
    [Node] private ProjectileScene _playerProjectile;
    [Node] private Marker2D _projectileResetMarker;
    [Node] private Path2D _flyingEnemyPath;
    [Node] private PathFollow2D _flyingEnemyPathFollow;
    
    private EnemyScene? _flyingEnemy;
    private Vector2 _initialPosition;

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
        _player.HeartDisplayComponent.HeartStartPosition = new Vector2(80, this.ViewportSize.Y - 60);

        _player.Position = new Vector2(this.ViewportSize.X / 2 - _player.Size.X / 2, this.ViewportSize.Y - 220);
        _enemyGrid.Position = new Vector2((this.ViewportSize.X - _enemyGrid.TotalWidth) / 2, 50);

        _playerProjectile.ResetMarker = _projectileResetMarker;
    }

    public override void _PhysicsProcess(double delta)
    {
        _projectileResetMarker.Position = new Vector2(
            _player.Position.X + _player.Size.X / 2 - _playerProjectile.Size.X / 2,
            _player.Position.Y - _playerProjectile.Size.Y);

        if (_flyingEnemy.IsValid())
        {
            _flyingEnemy.GlobalPosition = _flyingEnemyPathFollow.Position - _flyingEnemy.Size / 2;
        }
    }
    
    public void SendFlying(IEnemyView flying)
    {
        if (flying is not EnemyScene enemy || _flyingEnemy is not null)
        {
            return;
        }

        _flyingEnemyPath.Curve.ClearPoints();
        _flyingEnemyPath.Curve.AddPoint(enemy.GlobalCenter);
        _flyingEnemyPath.Curve.AddPoint(_player.Position.AddXY(_player.Size.X / 2, _player.Size.Y));
        
        _flyingEnemy = enemy;
        _initialPosition = _flyingEnemy.Position;

        _flyingEnemyPath.DrawOn(this);
        
        var tween = CreateTween();
        tween.TweenProperty(_flyingEnemyPathFollow, "progress_ratio", 1, 1.3);
        tween.TweenCallback(Callable.From(() =>
        {
            _flyingEnemyPathFollow.ProgressRatio = 0;
            _flyingEnemy.Position = _initialPosition;
            _flyingEnemy = null;
        }));
    }
}