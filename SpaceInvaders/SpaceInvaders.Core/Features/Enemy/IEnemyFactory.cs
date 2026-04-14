using SpaceInvaders.Core.Utils;

namespace SpaceInvaders.Core.Features.Enemy;

public interface IEnemyFactory
{
    List<IEnemyView> Create(ControllerBase parentController, IEnemyGridData<IEnemyData> gridData);
}