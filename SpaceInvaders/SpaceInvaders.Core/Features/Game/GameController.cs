using SpaceInvaders.Core.Features.Player;
using SpaceInvaders.Core.Utils;

namespace SpaceInvaders.Core.Features.Game;

public class GameController : ControllerBase<IMainView>
{
    private readonly PlayerController _playerController;

    public GameController(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public override void OnStart()
    {
        _playerController.SetView(View.Player);
        _playerController.OnStart();
    }

    public override void OnStop()
    {
        _playerController.OnStop();
    }
}