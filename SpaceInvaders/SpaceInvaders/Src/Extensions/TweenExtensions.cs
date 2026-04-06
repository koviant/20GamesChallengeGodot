using Godot;

namespace SpaceInvaders.Extensions;

public static class TweenExtensions
{
    extension(Tween tween)
    {
        public SignalAwaiter FinishedSignaled => tween.ToSignal(tween, Tween.SignalName.Finished);
    }
}