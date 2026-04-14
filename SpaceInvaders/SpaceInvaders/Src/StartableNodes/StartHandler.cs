using System;
using Godot;

namespace SpaceInvaders.StartableNodes;

public class StartHandler<TNode> where TNode: Node, IViewStart
{
    private readonly TNode _node;
    private bool _started;

    public StartHandler(TNode node)
    {
        _node = node;
        _node.SetPhysicsProcess(false);
    }
    
    public void Handle()
    {
        if (_started)
        {
            throw new InvalidOperationException("The node has already been started.");
        }
        
        _started = true;

        _node.ViewStart();
        _node.SetPhysicsProcess(true);
    }
}