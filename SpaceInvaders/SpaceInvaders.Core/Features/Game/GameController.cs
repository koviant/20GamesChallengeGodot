using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using SpaceInvaders.Core.Features.Enemy;
using SpaceInvaders.Core.Features.Enemy.Interfaces;
using SpaceInvaders.Core.Features.Player;
using SpaceInvaders.Core.Infrastructure;
using SpaceInvaders.Framework.Controllers;
using SpaceInvaders.Framework.Utils;

namespace SpaceInvaders.Core.Features.Game;

public class GameController : ControllerBase<IGameView>
{
    private readonly EnemyGridController _enemyGridController;
    private readonly GlobalEventBus _eventBus;
    private readonly PlayerController _playerController;
    private readonly ProjectileController _projectileController;

    public GameController(
        PlayerController playerController,
        ProjectileController projectileController,
        EnemyGridController enemyGridController,
        GlobalEventBus eventBus)
    {
        AddChildController(playerController, projectileController, enemyGridController);

        _playerController = playerController;
        _projectileController = projectileController;
        _enemyGridController = enemyGridController;
        _eventBus = eventBus;
        _projectileController.Color = Color.Yellow;
    }

    protected override void BeforeChildControllersStart()
    {
        _playerController.SetView(View.Player);
        _projectileController.SetView(View.ProjectileView);
        _enemyGridController.SetView(View.EnemyGridView);
    }

    protected override void OnStart()
    {
        AutoSubscribe(_eventBus.ShotPressed, OnShotPressed);
        AutoSubscribe(View.ProjectileView.Hit, ProjectileViewOnHit);
        AutoSubscribe(_eventBus.StartEnemyMoving, SendFlying);
    }

    private void SendFlying(Unit _)
    {
        var flying = _enemyGridController.EnemyViews[3];
        View.SendFlying(flying);
    }

    private void OnShotPressed(Unit _)
    {
        ShotHandler();
    }

    protected override void AfterViewStarted()
    {
        _playerController.Reset();
    }

    private void ProjectileViewOnHit(IView obj)
    {
        if (EnemyHit(obj, out var enemy))
        {
            enemy.Die();
        }

        _projectileController.Reset();
    }

    private bool EnemyHit(IView obj, [NotNullWhen(true)] out IEnemyView? enemy)
    {
        enemy = _enemyGridController.EnemyViews.FirstOrDefault(v => ReferenceEquals(v, obj));
        return enemy is not null;
    }

    private void ShotHandler()
    {
        if (_projectileController.State != ProjectileState.Flying)
        {
            _projectileController.Shoot();
        }
    }
}