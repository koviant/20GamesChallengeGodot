using SpaceInvaders.Core.Features.Enemy.Interfaces;
using SpaceInvaders.Framework.Controllers;

namespace SpaceInvaders.Core.Features.Enemy;

public class EnemyController : ControllerBase<IEnemyView>
{
    public EnemyType Type { get; set; }
}