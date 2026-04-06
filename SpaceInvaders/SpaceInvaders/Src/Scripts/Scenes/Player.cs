using System;
using System.Collections.Generic;
using Godot;
using GodotUtilities;
using SpaceInvaders.Core.Features.Player;
using SpaceInvaders.Core.Utils;
using SpaceInvaders.Extensions;
using SpaceInvaders.Scripts.Components;
using Animation = SpaceInvaders.Utils.Animation;

namespace SpaceInvaders.Scripts.Scenes;

[Scene]
public partial class Player : RigidBody2D, IPlayerView
{
	[Signal] public delegate void DeathAnimationFinishedEventHandler();
	
	[Node] private AnimationComponent _animationComponent;
	[Node] private HeartDisplayComponent _heartDisplayComponent;
	[Node] private HorizontalMovementComponent _horizontalMovementComponent;
	
	private const string AnimationHeartLost = nameof(AnimationHeartLost);
	private const string AnimationHeartBeating = nameof(AnimationHeartBeating);
	private const string AnimationNoneHeartsLeft = nameof(AnimationNoneHeartsLeft);

	[Export] public bool SkipDeathAnimation { get; set; }

	public System.Numerics.Vector2 Speed { get; set; }

	public HeartDisplayComponent HeartDisplayComponent
	{
		get => _heartDisplayComponent;
		set => _heartDisplayComponent = value;
	}
	
	public IHorizontalMovementComponent HorizontalMovementComponent => _horizontalMovementComponent;

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

	public override void _IntegrateForces(PhysicsDirectBodyState2D state)
	{
		var converted = Speed.ToGodotVector();
		if (state.LinearVelocity != converted)
		{
			state.LinearVelocity = converted;
		}
	}
	
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
		var heart = HeartDisplayComponent.LastFullHeart;

		return Animation.Beating(
			heart,
			[0.1f, 0.2f, 0.2f, 0.2f]
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

	private void OnBlipingFinished() => EmitSignalDeathAnimationFinished();
}