namespace SpaceInvaders.Core.Features.Enemy;

public interface IEnemyData : ICellData
{
    EnemyType Type { get; set; }
}