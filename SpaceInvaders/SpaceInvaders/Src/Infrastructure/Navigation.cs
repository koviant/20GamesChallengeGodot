using Godot;
using GodotUtilities;
using SpaceInvaders.Core.Features.Game;
using SpaceInvaders.Core.Infrastructure;
using SpaceInvaders.Features.Game;
using SpaceInvaders.Framework.Controllers;
using SpaceInvaders.Framework.Infrastructure;
using SpaceInvaders.Framework.Utils;

namespace SpaceInvaders.Infrastructure;

public class Navigation(IServiceLocator serviceLocator) : INavigation
{
    public void NavigateToGame()
    {
        var scene = GD.Load<PackedScene>("res://Src/Features/Game/game.tscn").Instantiate<GameScene>();
        var controller = serviceLocator.Get<GameController>(); 
        
        Bind(controller, scene);

        serviceLocator.Get<App>().AddChildDeferred(scene);
    }

    private void Bind<TView, TViewImpl>(ControllerBase<TView> controller, TViewImpl view)
        where TViewImpl : Node, TView
        where TView : IView
    {
        controller.SetView(view);

        view.Ready += controller.Start;
        view.TreeExited += controller.Stop;
    }
}