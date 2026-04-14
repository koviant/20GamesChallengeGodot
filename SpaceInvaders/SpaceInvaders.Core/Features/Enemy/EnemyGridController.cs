using SpaceInvaders.Core.Features.Enemy.Interfaces;
using SpaceInvaders.Framework.Controllers;
using SpaceInvaders.Framework.Utils;

namespace SpaceInvaders.Core.Features.Enemy;

public class EnemyGridController(IEnemyFactory enemyFactory, IEnemyDataReader dataReader)
    : ControllerBase<IEnemyGridView>
{
    private const int HSpeed = 100;

    public EnemyGridState State
    {
        get;
        set
        {
            field = value;
            View.State = value;
        }
    }

    public List<IEnemyView> EnemyViews { get; private set; } = new();

    protected override void OnStart()
    {
        View.HSpeed = HSpeed;
        State = EnemyGridState.MovingLeft;

        CreateEnemies();
        
        AutoSubscribe(View.HitBorder, ViewOnHitBorder);
    }

    private void CreateEnemies()
    {
        var gridData = dataReader.GetGridData();
        EnemyViews = enemyFactory.Create(this, gridData);
        View.Data = gridData;
        View.ClearGrid();
        View.SetEnemies(EnemyViews);
    }

    private void ViewOnHitBorder(Unit _)
    {
        if (State is EnemyGridState.Stopped)
        {
            return;
        }

        State = State switch
        {
            EnemyGridState.MovingRight => EnemyGridState.MovingLeft,
            EnemyGridState.MovingLeft => EnemyGridState.MovingRight,
            _ => throw new ArgumentOutOfRangeException(nameof(State), State, ""),
        };
    }
}