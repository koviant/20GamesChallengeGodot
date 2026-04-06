using System.Diagnostics;

namespace SpaceInvaders.Core.Utils;

public class HealthComponent
{
    public event Action? OnLifeLost;
    public event Action? OnLastLifeLeft;
    public event Action? OnDied;

    public bool Alive => LifeCount > 0;

    public int LifeCount { get; private set; }

    public void Reset(int maxLifeCount)
    {
        LifeCount = maxLifeCount;
    }

    public void DecreaseLifeCount()
    {
        Debug.Assert(LifeCount > 0, $"Calling {nameof(DecreaseLifeCount)} on invalid life count");

        LifeCount--;

        OnLifeLost?.Invoke();

        if (LifeCount is 1)
        {
            OnLastLifeLeft?.Invoke();
        }
        else if (LifeCount is 0)
        {
            OnDied?.Invoke();
        }
    }
}