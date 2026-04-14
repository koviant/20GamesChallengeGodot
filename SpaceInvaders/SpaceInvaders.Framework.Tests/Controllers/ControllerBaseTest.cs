using SpaceInvaders.Framework.Controllers;
using SpaceInvaders.Framework.Events;
using SpaceInvaders.Framework.Tests.Controllers.Fake;

namespace SpaceInvaders.Framework.Tests.Controllers;

[TestFixture]
[TestOf(typeof(ControllerBase))]
public class ControllerBaseTest
{
    [Test]
    public void Stop_ClearsMultipleEventSubscriptions()
    {
        // Arrange
        var controller = new FakeController();
        const int subscriptionCount1 = 10;
        var event1 = new InvokableEvent(); 
        controller.Subscribe(event1, subscriptionCount1);

        event1.Invoke();
        
        Assert.That(controller.Counter, Is.EqualTo(subscriptionCount1));
        
        const int subscriptionCount2 = 17;
        var event2 = new InvokableEvent(); 
        controller.Subscribe(event2, subscriptionCount2);
        
        event1.Invoke();
        event2.Invoke();

        const int expectedSubscriptionCount = 2*subscriptionCount1 + subscriptionCount2;
        Assert.That(controller.Counter, Is.EqualTo(expectedSubscriptionCount));
        
        // Act
        controller.Stop();
        event1.Invoke();
        event2.Invoke();
        
        // Assert
        Assert.That(controller.Counter, Is.EqualTo(expectedSubscriptionCount));
    }
     
    [TestCase(1)]
    [TestCase(10)]
    public void Stop_ClearsSingleEventSubscription(int subscriptionCounter)
    {
        // Arrange
        var controller = new FakeController();
        const int subscriptionCount1 = 1;
        var event1 = new InvokableEvent(); 
        controller.Subscribe(event1, subscriptionCount1);

        event1.Invoke();
        Assert.That(controller.Counter, Is.EqualTo(subscriptionCount1));
        
        // Act
        controller.Stop();
        event1.Invoke();
        
        // Assert
        Assert.That(controller.Counter, Is.EqualTo(subscriptionCount1));
    }
}