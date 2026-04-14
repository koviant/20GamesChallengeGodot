using System.Numerics;
using SpaceInvaders.Core.Features.Enemy.Interfaces;

namespace SpaceInvaders.Features.Enemy;

public class EnemyViewFactory : IEnemyViewFactory
{
    public IEnemyView Create(Vector2 size, IEnemyData data)
    {
        return EnemyScene.Create(size, data);
    }
}