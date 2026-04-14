namespace SpaceInvaders.Core.Features.Enemy.Interfaces;

public interface IEnemyDataReader
{
    IEnemyGridData<IEnemyData> GetGridData();
}