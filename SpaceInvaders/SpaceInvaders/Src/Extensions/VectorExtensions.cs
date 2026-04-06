namespace SpaceInvaders.Extensions;

public static class VectorExtensionMethods
{
    extension(System.Numerics.Vector2 v)
    {
        public Godot.Vector2 ToGodotVector() => new(v.X, v.Y);
    }
}