using System;
using System.Collections.Generic;
using Godot;
using GodotUtilities;
using SpaceInvaders.Components;
using SpaceInvaders.Core.Features.Game.Player;
using SpaceInvaders.Core.Utils;
using SpaceInvaders.Extensions;
using Animation = SpaceInvaders.Utils.Animation;
using Vector2 = Godot.Vector2;

namespace SpaceInvaders.Features.Game;

[Scene]
public partial class Player : RigidBody2D, IPlayerView
{
    private const string AnimationHeartLost = nameof(AnimationHeartLost);
    private const string AnimationHeartBeating = nameof(AnimationHeartBeating);
    private const string AnimationNoneHeartsLeft = nameof(AnimationNoneHeartsLeft);

    [Node] private AnimationComponent _animationComponent;
    [Node] private CollisionShape2D _collisionShape2D;
    [Node] private PlayerControllerComponent _controllerComponent;
    [Node] private HeartDisplayComponent _heartDisplayComponent;

    [Export] public bool SkipDeathAnimation { get; set; }
    public Vector2 Size => _collisionShape2D.Shape.GetRect().Size;

    public HeartDisplayComponent HeartDisplayComponent => _heartDisplayComponent;
    public event Action? DeathAnimationFinished;

    public event Action? Hit;

    public System.Numerics.Vector2 Speed { get; set; }

    public IPlayerControllerComponent ControllerComponent => _controllerComponent;

    public void PlayHeartLostAnimation()
    {
        _animationComponent.PlayAndMonitor(AnimationHeartLost);
    }

    public void PlayHeartBeatingAnimation()
    {
        _animationComponent.PlayAndMonitor(AnimationHeartBeating);
    }

    public void PlayNoneHeartsLeftAnimation()
    {
        _animationComponent.Play(AnimationNoneHeartsLeft);
    }

    public void Reset(int maxLifeCount)
    {
        HeartDisplayComponent.Reset(maxLifeCount);
    }

    public override void _Ready()
    {
        WireNodes();

        var animations = new Dictionary<string, Func<Tween>>
        {
            [AnimationHeartLost] = StartHeartLostAnimation,
            [AnimationHeartBeating] = StartHeartBeatingAnimation,
            [AnimationNoneHeartsLeft] = StartBlipingHeartsAnimation,
        };

        _animationComponent.Setup(animations);
    }

    public override void _Input(InputEvent e)
    {
        if (e is InputEventMouseButton { ButtonIndex: MouseButton.Left, Pressed: true })
        {
            Hit?.Invoke();
        }
    }

    public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
        state.LinearVelocity = Speed.ToGodotVector();
    }

    private Tween StartHeartLostAnimation()
    {
        if (SkipDeathAnimation)
        {
            return CreateTween();
        }

        var heart = HeartDisplayComponent.LastFullHeart;
        return Animation.ScaleToZero(heart, 0.2f);
    }

    private Tween StartHeartBeatingAnimation()
    {
        var lostHeartAnimationTween = _animationComponent.GetAnimationTween(AnimationHeartLost);

        return Animation.Beating(
            HeartDisplayComponent.FirstFullHeart,
            [0.1f, 0.2f, 0.2f, 0.2f],
            lostHeartAnimationTween
        );
    }

    private Tween StartBlipingHeartsAnimation()
    {
        _animationComponent.CancelRunningAnimation(AnimationHeartBeating);

        var bliping = Animation.Bliping(
            HeartDisplayComponent.EmptyHearts,
            [0.2f, 0.2f]
        );

        bliping.ConnectOneShot(Tween.SignalName.Finished, Callable.From(OnBlipingFinished));

        return bliping;
    }

    private void OnBlipingFinished()
    {
        DeathAnimationFinished?.Invoke();
    }
}