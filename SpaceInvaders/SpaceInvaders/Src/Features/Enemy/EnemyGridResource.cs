using Godot;
using SpaceInvaders.Core.Features.Enemy.Interfaces;

namespace SpaceInvaders.Features.Enemy;

[GlobalClass]
public partial class EnemyGridResource : Resource, IEnemyGridData<EnemyResource>
{
    [Export] public EnemyResource[] Items { get; set; } = [];

    [Export] public int CellHeight { get; set; }

    [Export] public int CellWidth { get; set; }

    [Export] public int HSpacing { get; set; }

    [Export] public int VSpacing { get; set; }

    [Export] public int RowCount { get; set; }

    [Export] public int ColumnCount { get; set; }

    public EnemyResource this[int row, int column] => Items[row * RowCount + column];
}