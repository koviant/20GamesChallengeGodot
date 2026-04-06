using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Godot;
using SpaceInvaders.Extensions;

namespace SpaceInvaders.Scripts.Components;

[GlobalClass]
public partial class AnimationComponent : Node
{
    private Dictionary<string, Func<Tween>> _animations = new();

    private readonly Dictionary<string, WeakReference<Tween>> _runningAnimations = new();

    public void Setup(Dictionary<string, Func<Tween>> animations)
    {
        _animations = animations;
    }

    public void PlayAndMonitor(string key)
    {
        var tween = CreateTweenInternal(key);
        Monitor(key, tween);
    }

    public void Play(string key)
    {
        CreateTweenInternal(key);
    }

    public async Task AnimationCompletion(string key)
    {
        AssertValidKey(key);

        if (!_runningAnimations.TryGetValue(key, out var animation))
        {
            return;
        }

        if (animation.TryGetTarget(out var tween) && tween.IsRunning())
        {
            await tween.FinishedSignaled;
        }
    }

    public void CancelRunningAnimation(string key)
    {
        AssertValidKey(key);

        if (!_runningAnimations.TryGetValue(key, out var weakReference))
        {
            return;
        }

        if (weakReference.TryGetTarget(out var tween) && tween.IsRunning())
        {
            tween.Kill();
        }
    }

    private void Monitor(string key, Tween tween)
    {
        AssertValidKey(key);
        _runningAnimations[key] = new WeakReference<Tween>(tween);
    }

    private Tween CreateTweenInternal(string key)
    {
        AssertValidKey(key);

        var tweenFactory = _animations[key];
        var tween = tweenFactory.Invoke();

        return tween ?? throw new Exception("Tween factory returned null");
    }

    private void AssertValidKey(string key)
    {
        Debug.Assert(_animations.ContainsKey(key), $"Provided key {key} is not defined");
    }
}