using SpaceInvaders.Framework.Events;
using SpaceInvaders.Framework.Utils;

namespace SpaceInvaders.Framework.Controllers;

public abstract class ControllerBase
{
    private readonly List<ControllerBase> _childControllers = new();
    private CompositeDisposable? _disposables;
    
    public void AddChildController(ControllerBase controller)
    {
        _childControllers.Add(controller);
    }

    public void AddChildController(params ControllerBase[] controllers)
    {
        foreach (var controller in controllers)
        {
            AddChildController(controller);
        }
    }

    protected virtual void BeforeChildControllersStart()
    {
    }

    protected void AutoSubscribe<T>(Event<T> e, Action<T> action)
    {
        _disposables ??= new CompositeDisposable();
        _disposables.Add(e.Subscribe(action));
    }

    public virtual void Start()
    {
        BeforeChildControllersStart();

        foreach (var controller in _childControllers)
        {
            controller.Start();
        }

        OnStart();
    }

    protected virtual void OnStart()
    {
    }

    public void Stop()
    {
        foreach (var controller in _childControllers)
        {
            controller.Stop();
        }

        _childControllers.Clear();
        _disposables?.Dispose();

        OnStop();
    }

    protected virtual void OnStop()
    {
    }
}