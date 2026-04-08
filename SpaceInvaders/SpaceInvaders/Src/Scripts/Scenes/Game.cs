using Godot;
using GodotUtilities;
using SpaceInvaders.Core.Features.Game;
using SpaceInvaders.Core.Features.Player;
using SpaceInvaders.Extensions;

namespace SpaceInvaders.Scripts.Scenes;

[Scene]
public partial class Game : Node2D, IGameView
{
    [Node] private CanvasLayer _hud;
    [Node] private Player _player;
    [Node] private Projectile _playerProjectile;

    public IPlayerView Player => _player;
    public IProjectileView ProjectileView => _playerProjectile;

    public override void _Ready()
    {
        WireNodes();

        _player.HeartDisplayComponent.HudLayer = _hud;
        _player.HeartDisplayComponent.HeartStartPosition = new Vector2(100, this.ViewportSize.Y - 80);

        _player.GlobalPosition = new Vector2(this.ViewportSize.X / 2 - _player.Size.X / 2, this.ViewportSize.Y - 200);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        ProcessPlayerProjectile(delta);
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