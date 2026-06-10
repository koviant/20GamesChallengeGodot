using Godot;

namespace SpaceInvaders.Extensions;

public static class VectorExtensions
{
    extension(Vector2 v)
    {
        public Vector2 WithX(float x)
        {
            return new Vector2(x, v.Y);
        }
        
        public Vector2 AddXY(float x, float y)
        {
            return new Vector2(v.X + x, v.Y + y);
        }
    }
}