using Jab;
using SpaceInvaders.Core.Features.Game;
using SpaceInvaders.Core.Features.Game.Player;
using SpaceInvaders.Core.Infrastructure;

namespace SpaceInvaders;

[ServiceProvider]
[Transient<PlayerController>]
[Transient<ProjectileController>]
[Singleton<INavigation, Navigation>]
[Singleton<GlobalEventBus>]
[Transient<GameController>]
public partial class ServiceLocator
{
    public static ServiceLocator Instance { get; } = new();
}