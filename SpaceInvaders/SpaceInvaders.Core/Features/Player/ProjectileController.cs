using System.Drawing;
using System.Numerics;
using SpaceInvaders.Framework.Controllers;
using static SpaceInvaders.Core.Features.Player.ProjectileState;

namespace SpaceInvaders.Core.Features.Player;

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

    protected override void OnStart()
    {
        View.Speed = Speed;
        View.State = State;
        View.Color = Color;

        View.OutOfBounds += Reset;
    }

    public void Shoot()
    {
        State = Flying;
    }

    public void Reset()
    {
        State = Ready;
    }

    protected override void OnStop()
    {
        View.OutOfBounds -= Reset;
    }
}

public enum ProjectileState
{
    Flying,
    Ready,
}