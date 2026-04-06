namespace SpaceInvaders.Core.Utils;

public abstract class ControllerBase<TView> where TView : IView
{
    protected TView View { get; private set; }

    public void SetView(TView view)
    {
        View = view;
    }
    
    public virtual void OnStart()
    {
    }

    public virtual void OnStop()
    {
    }
}