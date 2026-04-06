using System.Numerics;
using SpaceInvaders.Core.Utils;

namespace SpaceInvaders.Core.Features.Player;

public class PlayerController : ControllerBase<IPlayerView>
{
    private readonly HealthComponent _healthComponent;
    private IHorizontalMovementComponent _movementComponent;
    
	private readonly Vector2 _speed = new(800, 0);

    public int MaxLifeCount { get; set; } = 3;
    
    public PlayerController()
    {
        _healthComponent = new();
    }
    
    public override void OnStart()
    {
        _movementComponent = View.HorizontalMovementComponent;
        _movementComponent.OnMovement += OnMovementHandler;

        _healthComponent.OnLifeLost += OnHealthComponentOnOnLifeLost;
        _healthComponent.OnLastLifeLeft += OnHealthComponentOnOnLastLifeLeft;
        _healthComponent.OnDied += OnHealthComponentOnOnDied;
        
        View.Reset(MaxLifeCount);
    }

    private void OnHealthComponentOnOnDied()
    {
        View.PlayNoneHeartsLeftAnimation();
    }

    private void OnHealthComponentOnOnLastLifeLeft()
    {
        View.PlayHeartBeatingAnimation();
    }

    private void OnHealthComponentOnOnLifeLost()
    {
        View.PlayHeartLostAnimation();
    }

    private void OnMovementHandler(Movement movement)
    {
        var multiplier = movement switch
        {
            Movement.None => 0,
            Movement.Left => -1,
            Movement.Right => 1,
            _ => throw new ArgumentOutOfRangeException(nameof(movement), movement, null)
        };
        
        View.Speed = _speed * multiplier;
    }

    public override void OnStop()
    {
        _movementComponent.OnMovement -= OnMovementHandler;
        
        _healthComponent.OnLifeLost -= OnHealthComponentOnOnLifeLost;
        _healthComponent.OnLastLifeLeft -= OnHealthComponentOnOnLastLifeLeft;
        _healthComponent.OnDied -= OnHealthComponentOnOnDied;
    }
}