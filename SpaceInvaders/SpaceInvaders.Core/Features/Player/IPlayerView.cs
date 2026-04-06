using System.Numerics;
using SpaceInvaders.Core.Utils;

namespace SpaceInvaders.Core.Features.Player;

public interface IPlayerView : IView
{
	Vector2 Speed { get; set; }
	IHorizontalMovementComponent HorizontalMovementComponent { get; }
    
    void PlayHeartLostAnimation();
    void PlayHeartBeatingAnimation();
    void PlayNoneHeartsLeftAnimation();
    void Reset(int maxLifeCount);
}