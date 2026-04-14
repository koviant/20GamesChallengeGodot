namespace SpaceInvaders.Framework.Utils;

public class CompositeDisposable : IDisposable
{
    private bool _disposed;
    private readonly List<IDisposable> _disposables = new();

    public void Add(IDisposable disposable)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        _disposables.Add(disposable);
    }
    
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }
        
        _disposed = true;
        
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}