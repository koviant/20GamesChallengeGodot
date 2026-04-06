using Jab;
using SpaceInvaders.Core;
using SpaceInvaders.Core.Features.Player;
using SpaceInvaders.Core.Infrastructure;

namespace SpaceInvaders;

[ServiceProvider]
[Import<ICoreServiceLocator>]
[Transient<PlayerController>]
[Singleton<INavigation, Navigation>]
public partial class ServiceLocator
{
    public static ServiceLocator Instance { get; } = new();
}