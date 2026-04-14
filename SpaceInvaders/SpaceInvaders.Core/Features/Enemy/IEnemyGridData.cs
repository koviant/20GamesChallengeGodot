namespace SpaceInvaders.Core.Features.Enemy;

public interface IEnemyGridData<out TEnemyData> : IGridData<TEnemyData> where TEnemyData : IEnemyData;