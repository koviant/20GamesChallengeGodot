using SpaceInvaders.Framework.Utils;

namespace SpaceInvaders.Framework.Events;

public class Event<TArg>
{
    protected readonly List<Action<TArg>> Actions = new();
    
    public IDisposable Subscribe(Action<TArg> action)
    {
        Actions.Add(action);
        return new OnDisposeAction(() => Unsubscribe(action));
    }
    
    public void Unsubscribe(Action<TArg> action)
    {
        Actions.Remove(action);
    }
}