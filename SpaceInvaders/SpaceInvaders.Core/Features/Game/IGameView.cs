using SpaceInvaders.Core.Features.Enemy;
using SpaceInvaders.Core.Features.Game.Player;
using SpaceInvaders.Core.Utils;

namespace SpaceInvaders.Core.Features.Game;

public interface IGameView : IView
{
    IPlayerView Player { get; }
    IProjectileView ProjectileView { get; }
    IEnemyGridView EnemyGridView { get; }
}