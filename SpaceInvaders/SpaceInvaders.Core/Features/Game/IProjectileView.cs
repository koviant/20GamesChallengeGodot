using System.Drawing;
using System.Numerics;
using SpaceInvaders.Core.Utils;

namespace SpaceInvaders.Core.Features.Game;

public interface IProjectileView : IView
{
    event Action? Hit;
    event Action? OutOfBounds;
    
    Color Color { get; set; }
    Vector2 Speed { get; set; }
    ProjectileState State { get; set; } 
}