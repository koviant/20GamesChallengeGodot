using SpaceInvaders.Core.Features.Player;

namespace SpaceInvaders.Core.Infrastructure;

public class GlobalEventBus
{
    public event Action? ShotPressed;
    public event Action<Movement>? MovementPressed;
    
    public void OnShotPressed() => ShotPressed?.Invoke();
    public void OnMovementPressed(Movement m) => MovementPressed?.Invoke(m);
}