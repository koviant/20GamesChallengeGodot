using SpaceInvaders.Core.Features.Enemy.Interfaces;

namespace SpaceInvaders.Features.Enemy;

public class EnemyResourceDataReader : IEnemyDataReader
{
    public IEnemyGridData<IEnemyData> GetGridData()
    {
        return new EnemyGridResource
        {
            CellWidth = 80,
            CellHeight = 40,
            HSpacing = 30,
            VSpacing = 10,
            RowCount = 2,
            ColumnCount = 3,
            Items =
            [
                new EnemyResource(),
                new EnemyResource(),
                new EnemyResource(),
                new EnemyResource(),
                new EnemyResource(),
                new EnemyResource(),
            ],
        };
    }
}