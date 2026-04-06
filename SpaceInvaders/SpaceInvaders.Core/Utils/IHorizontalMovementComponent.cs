using SpaceInvaders.Core.Features.Player;

namespace SpaceInvaders.Core.Utils;

public interface IHorizontalMovementComponent
{
    event Action<Movement>? OnMovement;
}