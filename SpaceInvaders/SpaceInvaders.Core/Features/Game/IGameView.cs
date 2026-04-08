using SpaceInvaders.Core.Features.Player;
using SpaceInvaders.Core.Utils;

namespace SpaceInvaders.Core.Features.Game;

public interface IGameView : IView
{ 
    IPlayerView Player { get; }
    IProjectileView ProjectileView { get; }
}