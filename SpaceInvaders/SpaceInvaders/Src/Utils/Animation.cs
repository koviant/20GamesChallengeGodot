using System.Collections.Generic;
using System.Diagnostics;
using Godot;

namespace SpaceInvaders.Utils;

public static class Animation
{
    public static Tween ScaleToZero(Node2D view, float duration)
    {
        var tween = view.CreateTween();
        tween.TweenProperty(view, "scale", Vector2.Zero, duration);

        return tween;
    }

    public static Tween Beating(Node2D view, List<float> pattern, Tween? attach = null)
    {
        Debug.Assert(pattern.Count == 4, "pattern should contain exactly 4 items");

        var tween = attach ?? view.CreateTween();

        tween.SetLoops();
        tween.TweenInterval(pattern[0]);
        tween.TweenProperty(view, "scale", Vector2.One * 0.7f, pattern[1]);
        tween.TweenInterval(pattern[2]);
        tween.TweenProperty(view, "scale", Vector2.One, pattern[3]);

        return tween;
    }

    public static Tween Bliping(IReadOnlyList<Node2D> views, List<float> pattern)
    {
        Debug.Assert(pattern.Count == 2, "pattern should contain exactly 2 items");

        var tween = views[0].CreateTween().SetLoops(5);

        tween.TweenCallback(Callable.From(HideAll)).SetDelay(pattern[0]);
        tween.TweenCallback(Callable.From(ShowAll)).SetDelay(pattern[1]);

        return tween;

        void ShowAll()
        {
            foreach (var s in views)
            {
                s.Show();
            }
        }

        void HideAll()
        {
            foreach (var s in views)
            {
                s.Hide();
            }
        }
    }
}