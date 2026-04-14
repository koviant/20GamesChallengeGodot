using SpaceInvaders.Framework.Events;
using SpaceInvaders.Framework.Utils;

namespace SpaceInvaders.Framework.Controllers;

public abstract class ControllerBase
{
    private readonly List<ControllerBase> _controllers = new();
    private CompositeDisposable? _disposables;
    
    public void AddChildController(ControllerBase controller)
    {
        _controllers.Add(controller);
    }

    public void AddChildController(params ControllerBase[] controllers)
    {
        foreach (var controller in controllers)
        {
            AddChildController(controller);
        }
    }

    protected virtual void SetChildControllersView()
    {
    }

    protected void AutoSubscribe<T>(Event<T> e, Action<T> action)
    {
        _disposables ??= new CompositeDisposable();
        _disposables.Add(e.Subscribe(action));
    }

    public virtual void Start()
    {
        SetChildControllersView();

        foreach (var controller in _controllers)
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
        foreach (var controller in _controllers)
        {
            controller.Stop();
        }

        _controllers.Clear();
        _disposables?.Dispose();

        OnStop();
    }

    protected virtual void OnStop()
    {
    }
}