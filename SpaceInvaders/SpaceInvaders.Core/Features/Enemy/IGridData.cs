namespace SpaceInvaders.Core.Features.Enemy;

public interface IGridData<out TCell> where TCell : ICellData
{
    TCell[] Items { get; }
    int RowCount { get; }
    int CellHeight { get; }
    TCell this[int row, int column] { get; }
    int CellWidth { get; }
    int HSpacing { get; }
    int VSpacing { get; }
    int ColumnCount { get; }
}