using System.Diagnostics;
using Godot;
using GodotUtilities;
using SpaceInvaders.Core.Features.Enemy;
using SpaceInvaders.Extensions;

namespace SpaceInvaders.Features.Enemy;

[Scene]
public partial class EnemyGrid : Node2D, IEnemyGridView
{
    private int _enemyCount;

    [Export]
    public EnemyGridResource? Data { get; set; } = new()
    {
        CellWidth = 80,
        CellHeight = 40,
        HSpacing = 30,
        VSpacing = 10,
        RowCount = 2,
        ColumnCount = 3,
        Items =
        [
            new EnemyResource(),
            new EnemyResource(),
            new EnemyResource(),
            new EnemyResource(),
            new EnemyResource(),
            new EnemyResource(),
        ],
    };
    
    public EnemyGridState State { get; set; }

    public float TotalWidth
    {
        get
        {
            Debug.Assert(Data is not null);
            return Data.ColumnCount * Data.CellWidth + (Data.ColumnCount - 1) * Data.HSpacing;
        }
    }

    public override void _Ready()
    {
        WireNodes();
    }
    
    public void Reset()
    {
        Debug.Assert(Data is not null);
        Debug.Assert(Data.RowCount * Data.ColumnCount == Data.Items.Length);
        ClearGrid();
        CreateEnemies();
    }

    private void CreateEnemies()
    {
        Debug.Assert(Data is not null);
        var totalHeight = Data.RowCount * Data.CellHeight + (Data.RowCount - 1) * Data.VSpacing;

        Debug.Assert(TotalWidth > 0);
        Debug.Assert(this.ViewportSize.X > TotalWidth);
        Debug.Assert(totalHeight > 0);
        Debug.Assert(this.ViewportSize.Y / 2 > totalHeight);

        var brickSize = new Vector2(Data.CellWidth, Data.CellHeight);

        for (var row = 0; row < Data.RowCount; row++)
        {
            for (var col = 0; col < Data.ColumnCount; col++)
            {
                var data = Data[row, col];
                if (data.Type is EnemyType.Empty)
                {
                    continue;
                }

                _enemyCount++;

                var enemy = EnemyScene.Create(brickSize, data);
                enemy.Position = GetPosition(row, col);

                AddChild(enemy);
            }
        }
    }
    
    private Vector2 GetPosition(int row, int col)
    {
        Debug.Assert(Data is not null);
        return new Vector2(
            col * (Data.CellWidth + Data.HSpacing),
            row * (Data.CellHeight + Data.VSpacing)
        );
    }
    
    private void ClearGrid()
    {
        foreach (var child in GetChildren())
        {
            child.QueueFree();
        }
    }
}