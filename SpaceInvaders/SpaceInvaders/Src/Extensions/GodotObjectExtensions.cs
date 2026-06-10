using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Godot;

namespace SpaceInvaders.Extensions;

public static class GodotObjectExtensions
{
    public static bool IsValid([NotNullWhen(true)] this GodotObject? obj)
    {
        return GodotObject.IsInstanceValid(obj) && !obj.IsQueuedForDeletion();
    }

    extension(GodotObject? obj)
    {
        public void ConnectOneShot(StringName signal, Callable callable)
        {
            ArgumentNullException.ThrowIfNull(obj);
            var error = obj.Connect(signal, callable, (uint)GodotObject.ConnectFlags.OneShot);
            Debug.Assert(error is Error.Ok, $"Error={error} returned from Connect method");
        }
    }

    extension<T>(T obj) where T : GodotObject
    {
        public T? IfValid()
        {
            return obj.IsValid() ? obj : null;
        }
    } 
}