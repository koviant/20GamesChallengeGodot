using System.Drawing;
using SpaceInvaders.Core.Utils;

namespace SpaceInvaders.Core.Features.Enemy;

public class EnemyController : ControllerBase<IEnemyView>
{
    public EnemyType Type { get; set; }
}

public interface IEnemyView : IView
{
    
    public Color Color { get; set; }
}

public enum EnemyType
{
    Default,
    Empty,
}