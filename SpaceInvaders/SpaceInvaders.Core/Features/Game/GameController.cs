using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using SpaceInvaders.Core.Features.Enemy;
using SpaceInvaders.Core.Features.Enemy.Interfaces;
using SpaceInvaders.Core.Features.Game.Player;
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

    protected override void SetChildControllersView()
    {
        _playerController.SetView(View.Player);
        _projectileController.SetView(View.ProjectileView);
        _enemyGridController.SetView(View.EnemyGridView);
    }

    protected override void OnStart()
    {
        _eventBus.ShotPressed += ShotHandler;
        View.ProjectileView.Hit += ProjectileViewOnHit;
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

    protected override void OnStop()
    {
        _eventBus.ShotPressed -= ShotHandler;
    }

    private void ShotHandler()
    {
        if (_projectileController.State != ProjectileState.Flying)
        {
            _projectileController.Shoot();
        }
    }
}