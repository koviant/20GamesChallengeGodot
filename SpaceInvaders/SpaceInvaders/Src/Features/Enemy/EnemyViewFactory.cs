using System.Numerics;
using SpaceInvaders.Core.Features.Enemy;

namespace SpaceInvaders.Features.Enemy;

public class EnemyViewFactory : IEnemyViewFactory
{
    public IEnemyView Create(Vector2 size, IEnemyData data)
    {
        return EnemyScene.Create(size, data);
    }
}