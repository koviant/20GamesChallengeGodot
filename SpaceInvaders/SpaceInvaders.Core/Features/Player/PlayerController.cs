using System.Numerics;
using SpaceInvaders.Core.Infrastructure;
using SpaceInvaders.Core.Utils;
using SpaceInvaders.Framework.Controllers;

namespace SpaceInvaders.Core.Features.Player;

public class PlayerController : ControllerBase<IPlayerView>
{
    private readonly GlobalEventBus _eventBus;
    private readonly HealthComponent _healthComponent;

    private readonly Vector2 _speed = new(800, 0);

    public PlayerController(GlobalEventBus eventBus)
    {
        _eventBus = eventBus;
        _healthComponent = new HealthComponent();
    }

    public int MaxLifeCount { get; set; } = 3;

    protected override void OnStart()
    {
        View.ControllerComponent.Initialize(_eventBus);

        _eventBus.MovementPressed += MovementHandler;

        _healthComponent.OnLifeLost += OnHealthComponentOnOnLifeLost;
        _healthComponent.OnLastLifeLeft += OnHealthComponentOnOnLastLifeLeft;
        _healthComponent.OnDied += OnHealthComponentOnOnDied;

        View.Hit += _healthComponent.DecreaseLifeCount;
        View.DeathAnimationFinished += Reset;
    }

    public void Reset()
    {
        _healthComponent.Reset(MaxLifeCount);
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

    private void MovementHandler(Movement movement)
    {
        var multiplier = movement switch
        {
            Movement.None => 0,
            Movement.Left => -1,
            Movement.Right => 1,
            _ => throw new ArgumentOutOfRangeException(nameof(movement), movement, null),
        };

        View.Speed = _speed * multiplier;
    }

    protected override void OnStop()
    {
        _eventBus.MovementPressed -= MovementHandler;

        _healthComponent.OnLifeLost -= OnHealthComponentOnOnLifeLost;
        _healthComponent.OnLastLifeLeft -= OnHealthComponentOnOnLastLifeLeft;
        _healthComponent.OnDied -= OnHealthComponentOnOnDied;

        View.Hit -= _healthComponent.DecreaseLifeCount;
        View.DeathAnimationFinished -= Reset;
    }
}