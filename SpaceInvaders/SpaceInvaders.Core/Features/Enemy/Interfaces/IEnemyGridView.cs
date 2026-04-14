using SpaceInvaders.Framework.Utils;

namespace SpaceInvaders.Core.Features.Enemy.Interfaces;

public interface IEnemyGridView : IView
{
    EnemyGridState State { get; set; }
    int HSpeed { get; set; }
    IEnemyGridData<IEnemyData>? Data { get; set; }
    event Action HitBorder;
    void SetEnemies(List<IEnemyView> enemies);
    void ClearGrid();
}