using System.Diagnostics;
using SpaceInvaders.Framework.Events;
using SpaceInvaders.Framework.Utils;

namespace SpaceInvaders.Core.Utils;

public class HealthComponent
{
    private readonly InvokableEvent _onLifeLost = new();
    private readonly InvokableEvent _onLastLifeLeft = new();
    private readonly InvokableEvent _onDied = new();
    
    public Event<Unit> OnLifeLost => _onLifeLost;
    public Event<Unit> OnLastLifeLeft => _onLastLifeLeft;
    public Event<Unit> OnDied => _onDied;
    
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

        _onLifeLost.Invoke();

        if (LifeCount is 1)
        {
            _onLastLifeLeft.Invoke();
        }
        else if (LifeCount is 0)
        {
            _onDied.Invoke();
        }
    }
}