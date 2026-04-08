using SpaceInvaders.Core.Infrastructure;

namespace SpaceInvaders.Core.Utils;

public interface IPlayerControllerComponent
{
    void Initialize(GlobalEventBus eventBus);
}