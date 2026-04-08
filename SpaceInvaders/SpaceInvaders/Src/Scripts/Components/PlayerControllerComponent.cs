using System;
using Godot;
using SpaceInvaders.Core.Features.Player;
using SpaceInvaders.Core.Infrastructure;
using SpaceInvaders.Core.Utils;

namespace SpaceInvaders.Scripts.Components;

[GlobalClass]
public partial class PlayerControllerComponent : Node, IPlayerControllerComponent
{
    private GlobalEventBus? _eventBus;

    private GlobalEventBus EventBus => _eventBus ?? throw new InvalidOperationException("EventBus not set");

    public void Initialize(GlobalEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public override void _Input(InputEvent e)
    {
        if (e is InputEventMouseButton { ButtonIndex: MouseButton.Right, Pressed: true })
        {
            EventBus.OnShotPressed();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        var axis = Input.GetAxis("ui_left", "ui_right");
        var direction = axis switch
        {
            < 0 => Movement.Left,
            > 0 => Movement.Right,
            _ => Movement.None,
        };

        EventBus.OnMovementPressed(direction);
    }
}