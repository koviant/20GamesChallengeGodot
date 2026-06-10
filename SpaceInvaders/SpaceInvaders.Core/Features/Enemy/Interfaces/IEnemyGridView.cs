using SpaceInvaders.Framework.Events;
using SpaceInvaders.Framework.Utils;

namespace SpaceInvaders.Core.Features.Enemy.Interfaces;

public interface IEnemyGridView : IView
{
    EnemyGridState State { get; set; }
    int HSpeed { get; set; }
    Event<Unit> HitBorder { get; }
    float TotalWidth { get; set; }
    void SetEnemies(List<IEnemyView> enemies);
    void ClearGrid();
}