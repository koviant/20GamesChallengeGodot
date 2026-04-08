using Godot;

namespace SpaceInvaders.Extensions;

public static class NodeExtensions
{
    extension(Node node)
    {
        public Vector2 ViewportSize => node.GetViewport().GetVisibleRect().Size;
    }
}