using System.Drawing;
using System.Numerics;

namespace SpaceInvaders.Extensions;

public static class VectorExtensionMethods
{
    extension(Vector2 v)
    {
        public Godot.Vector2 ToGodotVector()
        {
            return new Godot.Vector2(v.X, v.Y);
        }
    }

    extension(Color c)
    {
        public Godot.Color ToGodotColor()
        {
            return new Godot.Color(c.R, c.G, c.B, c.A);
        }
    }

    extension(Godot.Color c)
    {
        public Color ToNativeColor()
        {
            return Color.FromArgb(c.A8, c.R8, c.G8, c.B8);
        }
    }
}