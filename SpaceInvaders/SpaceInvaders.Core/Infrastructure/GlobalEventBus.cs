using SpaceInvaders.Core.Features.Player;
using SpaceInvaders.Framework.Events;
using SpaceInvaders.Framework.Utils;

namespace SpaceInvaders.Core.Infrastructure;

public class GlobalEventBus
{
    private readonly InvokableEvent _shotPressed = new();
    private readonly InvokableEvent<Movement> _movementPressed = new(); 
    private readonly InvokableEvent _startEnemyMoving = new();
    
    public Event<Unit> ShotPressed  => _shotPressed;
    public Event<Movement> MovementPressed  => _movementPressed;
    public Event<Unit> StartEnemyMoving => _startEnemyMoving;

    public void OnShotPressed()
    {
        _shotPressed.Invoke();
    }

    public void OnMovementPressed(Movement m)
    {
        _movementPressed.Invoke(m);
    }

    public void OnStartEnemyMoving()
    {
        _startEnemyMoving.Invoke();
    }
}