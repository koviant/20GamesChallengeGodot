using System.Drawing;
using System.Numerics;
using SpaceInvaders.Core.Utils;

namespace SpaceInvaders.Core.Features.Game.Player;

public interface IProjectileView : IView
{
    Color Color { get; set; }
    Vector2 Speed { get; set; }
    ProjectileState State { get; set; }
    event Action<IView>? Hit;
    event Action? OutOfBounds;
}