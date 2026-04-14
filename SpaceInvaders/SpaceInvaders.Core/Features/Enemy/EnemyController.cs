using SpaceInvaders.Core.Utils;

namespace SpaceInvaders.Core.Features.Enemy;

public class EnemyController : ControllerBase<IEnemyView>
{
    public EnemyType Type { get; set; }
}