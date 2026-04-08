using System.Drawing;
using System.Numerics;
using SpaceInvaders.Core.Utils;
using static SpaceInvaders.Core.Features.Game.ProjectileState;

namespace SpaceInvaders.Core.Features.Game;

public class ProjectileController : ControllerBase<IProjectileView>
{
    public ProjectileState State
    {
        get;
        private set
        {
            field = value;
            View?.State = State;
        }
    } = Ready;

    public Vector2 Speed { get; set; } = new(0, -800);

    public Color Color { get; set; } = Color.DarkGray;

    public override void OnStart()
    {
        View.Speed = Speed;
        View.State = State;
        View.Color = Color;

        View.Hit += Reset;
        View.OutOfBounds += Reset;
    }

    public void Shoot()
    {
        State = Flying;
    }

    private void Reset()
    {
        State = Ready;
    }
}

public enum ProjectileState
{
    Flying,
    Ready,
}