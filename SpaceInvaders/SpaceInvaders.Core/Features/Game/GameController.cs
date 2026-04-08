using System.Drawing;
using SpaceInvaders.Core.Features.Player;
using SpaceInvaders.Core.Infrastructure;
using SpaceInvaders.Core.Utils;

namespace SpaceInvaders.Core.Features.Game;

public class GameController : ControllerBase<IGameView>
{
    private readonly PlayerController _playerController;
    private readonly ProjectileController _projectileController;
    private readonly GlobalEventBus _eventBus;

    public GameController(
        PlayerController playerController, 
        ProjectileController projectileController,
        GlobalEventBus eventBus)
    {
        _playerController = playerController;
        _projectileController = projectileController;
        _eventBus = eventBus;
        _projectileController.Color = Color.Yellow;
    }

    public override void OnStart()
    {
        _playerController.SetView(View.Player);
        _projectileController.SetView(View.ProjectileView);
        
        _eventBus.ShotPressed += ShotHandler;
        
        _playerController.OnStart();
        _projectileController.OnStart();
    }

    public override void OnStop()
    {
        _eventBus.ShotPressed -= ShotHandler;
        
        _playerController.OnStop();
        _projectileController.OnStop();
    }

    private void ShotHandler()
    {
        if (_projectileController.State != ProjectileState.Flying)
        {
            _projectileController.Shoot();
        }
    }
}