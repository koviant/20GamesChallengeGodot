using Godot;
using GodotUtilities;
using SpaceInvaders.Core.Features.Main;
using SpaceInvaders.Core.Infrastructure;
using SpaceInvaders.Core.Utils;
using SpaceInvaders.Scripts.Scenes;

namespace SpaceInvaders;


public class Navigation : INavigation
{
    public void NavigateToMain()
    {
        var scene = GD.Load<PackedScene>("res://Scenes/game.tscn").Instantiate<Game>();
        var controller = ServiceLocator.Instance.GetService<GameController>();
        Bind(controller, scene);
        
        App.Instance.GetTree().Root.AddChildDeferred(scene);
    }

    private void Bind<TView, TViewImpl>(ControllerBase<TView> controller, TViewImpl view) 
        where TViewImpl : Node, IView, TView 
        where TView : IView
    {
        controller.SetView(view);

        view.Ready += controller.OnStart;
        view.TreeExited += controller.OnStop;
    }
}