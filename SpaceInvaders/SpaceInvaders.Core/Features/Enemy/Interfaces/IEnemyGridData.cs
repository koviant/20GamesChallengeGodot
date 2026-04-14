using SpaceInvaders.Framework.Grid;

namespace SpaceInvaders.Core.Features.Enemy.Interfaces;

public interface IEnemyGridData<out TEnemyData> : IGridData<TEnemyData> where TEnemyData : IEnemyData;