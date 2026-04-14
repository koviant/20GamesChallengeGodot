namespace SpaceInvaders.Framework.Controllers;

public abstract class ControllerBase
{
    private readonly List<ControllerBase> _controllers = new();

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

        OnStop();
    }

    protected virtual void OnStop()
    {
    }
}