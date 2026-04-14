using System.Numerics;
using SpaceInvaders.Core.Utils;
using SpaceInvaders.Framework.Events;
using SpaceInvaders.Framework.Utils;

namespace SpaceInvaders.Core.Features.Player;

public interface IPlayerView : IView
{
    IPlayerControllerComponent ControllerComponent { get; }

    Vector2 Speed { get; set; }

    Event<Unit> Hit { get; }
    Event<Unit> DeathAnimationFinished { get; }

    void PlayHeartLostAnimation();
    void PlayHeartBeatingAnimation();
    void PlayNoneHeartsLeftAnimation();
    void Reset(int maxLifeCount);
}