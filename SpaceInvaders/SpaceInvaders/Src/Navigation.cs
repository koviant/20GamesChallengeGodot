using Godot;
using GodotUtilities;
using SpaceInvaders.Core.Features.Game;
using SpaceInvaders.Core.Infrastructure;
using SpaceInvaders.Core.Utils;
using Game = SpaceInvaders.Features.Game.Game;

namespace SpaceInvaders;

public class Navigation : INavigation
{
    public void NavigateToGame()
    {
        var scene = GD.Load<PackedScene>("res://Src/Features/Game/game.tscn").Instantiate<Game>();
        var controller = ServiceLocator.Instance.GetService<GameController>();
        Bind(controller, scene);

        App.App.Instance.GetTree().Root.AddChildDeferred(scene);
    }

    private void Bind<TView, TViewImpl>(ControllerBase<TView> controller, TViewImpl view)
        where TViewImpl : Node, TView
        where TView : IView
    {
        controller.SetView(view);

        view.Ready += controller.OnStart;
        view.TreeExited += controller.OnStop;
    }
}