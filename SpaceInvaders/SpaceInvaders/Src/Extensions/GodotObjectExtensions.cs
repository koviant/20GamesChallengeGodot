using System.Diagnostics;
using Godot;

namespace SpaceInvaders.Extensions;

public static class GodotObjectExtensions
{
    extension(GodotObject obj)
    {
        public void ConnectOneShot(StringName signal, Callable callable)
        {
            var error = obj.Connect(signal, callable, (uint)GodotObject.ConnectFlags.OneShot);
            Debug.Assert(error is Error.Ok, $"Error={error} returned from Connect method");
        }
    }
}