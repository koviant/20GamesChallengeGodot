using System;
using Godot;

namespace SpaceInvaders.StartableNodes;

public partial class StartableNode2D : Node2D, IViewStart
{
    private StartHandler<StartableNode2D>? _startHandler;

    public void Start()
    {
        if (_startHandler is null)
        {
            throw new InvalidOperationException("Start handler not initialized");
        }

        _startHandler.Handle();
    }

    public virtual void ViewStart()
    {
    }

    public sealed override void _Notification(int what)
    {
        base._Notification(what);

        if (what == NotificationEnterTree)
        {
            _startHandler = new StartHandler<StartableNode2D>(this);
        }
    }
}