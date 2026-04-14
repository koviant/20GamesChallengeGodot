using System;
using Godot;

namespace SpaceInvaders.StartableNodes;

public partial class StartableRigidBody2D : RigidBody2D, IViewStart
{
    private StartHandler<StartableRigidBody2D>? _startHandler;

    public sealed override void _Notification(int what)
    {
        base._Notification(what);

        if (what == NotificationEnterTree)
        {
            _startHandler = new StartHandler<StartableRigidBody2D>(this);
        }
    }

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
}