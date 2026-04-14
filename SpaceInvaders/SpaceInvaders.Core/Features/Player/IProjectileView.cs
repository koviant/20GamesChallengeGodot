using System.Drawing;
using System.Numerics;
using SpaceInvaders.Framework.Events;
using SpaceInvaders.Framework.Utils;

namespace SpaceInvaders.Core.Features.Player;

public interface IProjectileView : IView
{
    Color Color { get; set; }
    Vector2 Speed { get; set; }
    ProjectileState State { get; set; }
    Event<IView> Hit { get; }
    Event<Unit> OutOfBounds { get; }
}