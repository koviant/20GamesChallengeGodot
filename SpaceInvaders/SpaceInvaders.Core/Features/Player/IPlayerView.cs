using System.Numerics;
using SpaceInvaders.Core.Utils;
using SpaceInvaders.Framework.Utils;

namespace SpaceInvaders.Core.Features.Player;

public interface IPlayerView : IView
{
    IPlayerControllerComponent ControllerComponent { get; }

    Vector2 Speed { get; set; }
    event Action? Hit;
    event Action? DeathAnimationFinished;

    void PlayHeartLostAnimation();
    void PlayHeartBeatingAnimation();
    void PlayNoneHeartsLeftAnimation();
    void Reset(int maxLifeCount);
}