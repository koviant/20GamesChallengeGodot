using System.Numerics;
using SpaceInvaders.Core.Infrastructure;
using SpaceInvaders.Core.Utils;
using SpaceInvaders.Framework.Controllers;
using SpaceInvaders.Framework.Utils;

namespace SpaceInvaders.Core.Features.Player;

public class PlayerController(GlobalEventBus eventBus) : ControllerBase<IPlayerView>
{
    private readonly HealthComponent _healthComponent = new();

    private readonly Vector2 _speed = new(800, 0);

    public int MaxLifeCount { get; set; } = 3;

    protected override void OnStart()
    {
        View.ControllerComponent.Initialize(eventBus);

        AutoSubscribe(eventBus.MovementPressed, MovementHandler);
        AutoSubscribe(eventBus.MovementPressed, MovementHandler);
        AutoSubscribe(_healthComponent.OnLifeLost, OnHealthComponentOnOnLifeLost);
        AutoSubscribe(_healthComponent.OnLastLifeLeft, OnHealthComponentOnOnLastLifeLeft);
        AutoSubscribe(_healthComponent.OnDied, OnHealthComponentOnDied);
        AutoSubscribe(View.Hit, _ => _healthComponent.DecreaseLifeCount());
        AutoSubscribe(View.DeathAnimationFinished, _ => Reset());
    }

    public void Reset()
    {
        _healthComponent.Reset(MaxLifeCount);
        View.Reset(MaxLifeCount);
    }

    private void OnHealthComponentOnDied(Unit _)
    {
        View.PlayNoneHeartsLeftAnimation();
    }

    private void OnHealthComponentOnOnLastLifeLeft(Unit _)
    {
        View.PlayHeartBeatingAnimation();
    }

    private void OnHealthComponentOnOnLifeLost(Unit _)
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
}