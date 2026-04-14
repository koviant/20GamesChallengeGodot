using System;
using System.Diagnostics;
using Godot;
using SpaceInvaders.Core.Infrastructure;

namespace SpaceInvaders.Infrastructure;

public partial class App : Node2D
{
    public static App Instance
    {
        get => field ?? throw new InvalidOperationException("App instance not initialized");
        private set
        {
            if (field is not null)
            {
                throw new InvalidOperationException("App instance already initialized");
            }
            
            field = value;
        }
    }

    public App()
    {
        Instance = this;
    }

    public override void _Ready()
    {
        ServiceLocator.Instance.GetService<INavigation>().NavigateToGame();
    }
}