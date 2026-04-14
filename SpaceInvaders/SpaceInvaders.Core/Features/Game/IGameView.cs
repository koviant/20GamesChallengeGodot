using SpaceInvaders.Core.Features.Enemy.Interfaces;
using SpaceInvaders.Core.Features.Game.Player;
using SpaceInvaders.Framework.Utils;

namespace SpaceInvaders.Core.Features.Game;

public interface IGameView : IView
{
    IPlayerView Player { get; }
    IProjectileView ProjectileView { get; }
    IEnemyGridView EnemyGridView { get; }
}