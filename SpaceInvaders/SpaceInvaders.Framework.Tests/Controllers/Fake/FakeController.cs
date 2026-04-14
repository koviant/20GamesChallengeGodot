using SpaceInvaders.Framework.Controllers;
using SpaceInvaders.Framework.Events;
using SpaceInvaders.Framework.Utils;

namespace SpaceInvaders.Framework.Tests.Controllers.Fake;

public class FakeController : ControllerBase
{
    public int Counter { get; set; }
    
    public void Subscribe(Event<Unit> e, int subscriptionCount)
    {
        for (int i = 0; i < subscriptionCount; i++)
        {
            AutoSubscribe(e, _ => Counter++);
        }
    }
}