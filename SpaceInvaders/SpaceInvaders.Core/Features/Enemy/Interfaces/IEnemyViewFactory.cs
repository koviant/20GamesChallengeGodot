using System.Numerics;

namespace SpaceInvaders.Core.Features.Enemy.Interfaces;

public interface IEnemyViewFactory
{
    IEnemyView Create(Vector2 size, IEnemyData data);
}