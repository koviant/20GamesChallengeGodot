using Godot;

namespace SpaceInvaders.Scripts.Components;

[GlobalClass]
public partial class HorizontalMovementComponent : Node
{
    private Vector2 _velocity;
    private RigidBody2D _body;
    
    public void Setup(Vector2 velocity, RigidBody2D body)
    {
        _velocity = velocity;
        _body = body;
    }
    
    public override void _PhysicsProcess(double delta)
    {
        var direction = Input.GetAxis("ui_left", "ui_right");
        _body.LinearVelocity = direction * _velocity;
    }
}