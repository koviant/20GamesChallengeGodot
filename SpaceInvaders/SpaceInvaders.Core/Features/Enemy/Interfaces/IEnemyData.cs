using SpaceInvaders.Framework.Grid;

namespace SpaceInvaders.Core.Features.Enemy.Interfaces;

public interface IEnemyData : ICellData
{
    EnemyType Type { get; set; }
}