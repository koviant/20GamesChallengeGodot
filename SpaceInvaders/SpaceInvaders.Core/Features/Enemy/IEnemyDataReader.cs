namespace SpaceInvaders.Core.Features.Enemy;

public interface IEnemyDataReader
{
    IEnemyGridData<IEnemyData> GetGridData();
}