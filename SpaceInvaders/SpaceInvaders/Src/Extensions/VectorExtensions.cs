using Godot;

namespace SpaceInvaders.Extensions;

public static class VectorExtensions
{
    extension(Vector2 v)
    {
        public Vector2 WithX(float x) => new(x, v.Y);
    }
}