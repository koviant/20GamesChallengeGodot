namespace SpaceInvaders.Core.Features.Enemy;

public interface ICellData;

public interface IGridData<TCell> where TCell : ICellData
{
    TCell[] Items { get; }
    int RowCount { get;}
    int CellHeight { get; }
    TCell this[int row, int column] { get; }
}

public interface IEnemyData : ICellData
{
    EnemyType Type { get; set; }
}

public interface IEnemyGridData<TEnemyData> : IGridData<TEnemyData> where TEnemyData : IEnemyData;
