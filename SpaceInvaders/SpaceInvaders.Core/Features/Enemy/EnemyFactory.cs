using System.Numerics;
using SpaceInvaders.Core.Infrastructure;
using SpaceInvaders.Core.Utils;

namespace SpaceInvaders.Core.Features.Enemy;

public class EnemyFactory(
    IServiceLocator serviceLocator,
    IEnemyViewFactory viewFactory)
    : IEnemyFactory
{
    public List<IEnemyView> Create(ControllerBase parentController, IEnemyGridData<IEnemyData> gridData)
    {
        var enemies = new List<IEnemyView>();
        var enemySize = new Vector2(gridData.CellWidth, gridData.CellHeight);
        for (var row = 0; row < gridData.RowCount; row++)
        {
            for (var col = 0; col < gridData.ColumnCount; col++)
            {
                var data = gridData[row, col];
                if (data.Type is EnemyType.Empty)
                {
                    continue;
                }

                var enemy = viewFactory.Create(enemySize, data);
                var enemyController = serviceLocator.Get<EnemyController>();
                enemyController.SetView(enemy);
                parentController.AddChildController(enemyController);
                
                enemy.PlaceAt(GetPosition(gridData, row, col));

                enemies.Add(enemy);
            }
        }
        
        return enemies;
    }

    private Vector2 GetPosition(IEnemyGridData<IEnemyData> gridData, int row, int col)
    {
        return new Vector2(
            col * (gridData.CellWidth + gridData.HSpacing),
            row * (gridData.CellHeight + gridData.VSpacing)
        );
    }
}