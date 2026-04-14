using System.Drawing;
using System.Numerics;
using SpaceInvaders.Core.Utils;

namespace SpaceInvaders.Core.Features.Enemy;

public interface IEnemyView : IView
{
    public Color Color { get; set; }
    public void PlaceAt(Vector2 position);
    public void Die();
}