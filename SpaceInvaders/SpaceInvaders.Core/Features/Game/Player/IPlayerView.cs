using System.Numerics;
using SpaceInvaders.Core.Utils;

namespace SpaceInvaders.Core.Features.Player;

public interface IPlayerView : IView
{
	event Action? Hit;
	event Action? DeathAnimationFinished;
	
	IPlayerControllerComponent ControllerComponent { get; }
	
	Vector2 Speed { get; set; }
    
    void PlayHeartLostAnimation();
    void PlayHeartBeatingAnimation();
    void PlayNoneHeartsLeftAnimation();
    void Reset(int maxLifeCount);
}