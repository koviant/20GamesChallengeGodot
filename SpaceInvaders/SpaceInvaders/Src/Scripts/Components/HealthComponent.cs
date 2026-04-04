using System;
using Godot;

namespace SpaceInvaders.Scripts.Components;

[GlobalClass]
public partial class HealthComponent : Node
{
    [Signal]
    public delegate void LifeLostEventHandler();

    [Signal]
    public delegate void LastLifeLeftEventHandler();

    [Signal]
    public delegate void DiedEventHandler();

    public bool Alive => LifeCount > 0;

    [Export]
    public int LifeCount { get; private set; }

    public void Reset(int maxLifeCount)
    {
        LifeCount = maxLifeCount;
    }

    public void DecreaseLifeCount()
    {
        if (LifeCount < 0)
        {
            throw new InvalidOperationException($"Calling {nameof(DecreaseLifeCount)} on invalid life count");
        }

        LifeCount--;

        EmitSignal(SignalName.LifeLost);

        if (LifeCount is 1)
        {
            EmitSignal(SignalName.LastLifeLeft);
        }
        else if (LifeCount is 0)
        {
            EmitSignal(SignalName.Died);
        }
    }
}