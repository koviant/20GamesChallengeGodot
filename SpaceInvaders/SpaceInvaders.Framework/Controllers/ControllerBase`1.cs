using SpaceInvaders.Framework.Utils;

namespace SpaceInvaders.Framework.Controllers;

public abstract class ControllerBase<TView> : ControllerBase where TView : IView
{
    protected TView View { get; private set; }

    public void SetView(TView view)
    {
        View = view;
    }

    public sealed override void Start()
    {
        base.Start();

        View.Start();
        
        AfterViewStarted();
    }

    protected virtual void AfterViewStarted()
    {
    }
}