using SpaceInvaders.Framework.Controllers;

namespace SpaceInvaders.Core.Features.Enemy.Interfaces;

public interface IEnemyFactory
{
    List<IEnemyView> Create(ControllerBase parentController, IEnemyGridData<IEnemyData> gridData);
}