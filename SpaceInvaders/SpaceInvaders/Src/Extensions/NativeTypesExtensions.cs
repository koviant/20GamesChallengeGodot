namespace SpaceInvaders.Extensions;

public static class VectorExtensionMethods
{
    extension(System.Numerics.Vector2 v)
    {
        public Godot.Vector2 ToGodotVector() => new(v.X, v.Y);
    }

    extension(System.Drawing.Color c)
    {
        public Godot.Color ToGodotColor() => new(c.R, c.G, c.B, c.A);
    }

    extension(Godot.Color c)
    {
        public System.Drawing.Color ToNativeColor() => System.Drawing.Color.FromArgb(c.A8, c.R8, c.G8, c.B8);
    }
}