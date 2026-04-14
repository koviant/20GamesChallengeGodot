namespace SpaceInvaders.Framework.Utils;

public class OnDisposeAction(Action dispose) : IDisposable
{
    private bool _disposed;

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }
        
        _disposed = true;
        dispose.Invoke();
    }
}