using SpaceInvaders.Framework.Utils;

namespace SpaceInvaders.Framework.Events;

public class InvokableEvent<TArg> : Event<TArg>
{
    public void Invoke(TArg arg)
    {
        foreach (var action in Actions)
        {
            action.Invoke(arg);
        }
    }
}

public class InvokableEvent : InvokableEvent<Unit>
{
    public void Invoke()
    {
        Invoke(Unit.Default);
    }
}