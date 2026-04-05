using System;
using System.Collections.Generic;
using Godot;

namespace SpaceInvaders.Utils;

public static class Animation
{
    public static Tween ScaleToZero(Sprite2D sprite, float duration)
    {
        var tween = sprite.CreateTween();
        tween.TweenProperty(sprite, "scale", Vector2.Zero, duration);

        return tween;
    }

    public static Tween Beating(Sprite2D sprite, List<float> pattern)
    {
        if (pattern.Count != 4)
        {
            throw new ArgumentException("pattern should contain exactly 4 items");
        }

        var tween = sprite.CreateTween().SetLoops();

        tween.TweenInterval(pattern[0]);
        tween.TweenProperty(sprite, "scale", Vector2.One * 0.7f, pattern[1]);
        tween.TweenInterval(pattern[2]);
        tween.TweenProperty(sprite, "scale", Vector2.One, pattern[3]);

        return tween;
    }

    public static Tween Bliping(IReadOnlyList<Sprite2D> sprites, List<float> pattern)
    {
        if (pattern.Count != 2)
        {
            throw new ArgumentException("pattern should contain exactly 2 items");
        }

        var tween = sprites[0].CreateTween().SetLoops(5);

        tween.TweenCallback(Callable.From(HideAll)).SetDelay(pattern[0]);
        tween.TweenCallback(Callable.From(ShowAll)).SetDelay(pattern[1]);

        return tween;

        void ShowAll()
        {
            foreach (var s in sprites) s.Show();
        }

        void HideAll()
        {
            foreach (var s in sprites) s.Hide();
        }
    }
}