using System.Diagnostics;
using Godot;
using SpaceInvaders.Core.Infrastructure;

namespace SpaceInvaders.Infrastructure;

public partial class App : Node2D
{
    public static App Instance;

    public App()
    {
        Debug.Assert(Instance is null);
        Instance = this;
    }

    public override void _Ready()
    {
        ServiceLocator.Instance.GetService<INavigation>().NavigateToGame();
    }
}