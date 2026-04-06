using System;
using Godot;
using SpaceInvaders.Core.Features.Player;
using SpaceInvaders.Core.Utils;

namespace SpaceInvaders.Scripts.Components;

public partial class HorizontalMovementComponent : Node, IHorizontalMovementComponent
{
    public override void _PhysicsProcess(double delta)
    {
        var axis = Input.GetAxis("ui_left", "ui_right");
        var direction = axis switch
        {
            < 0 => Movement.Left,
            > 0 => Movement.Right,
            _ => Movement.None,
        };
        
        OnMovement?.Invoke(direction);
    }

    public event Action<Movement> OnMovement;
}