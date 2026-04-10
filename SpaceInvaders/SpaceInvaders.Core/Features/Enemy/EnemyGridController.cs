using SpaceInvaders.Core.Utils;
using SpaceInvaders.Features.Enemy;

namespace SpaceInvaders.Core.Features.Enemy;

public class EnemyGridController : ControllerBase<IEnemyGridView>
{
    public EnemyGridState GridState { get; set; }
}

public interface IEnemyGridView : IView
{
}