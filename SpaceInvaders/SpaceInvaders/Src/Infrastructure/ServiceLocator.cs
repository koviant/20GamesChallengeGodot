using Jab;
using SpaceInvaders.Core.Features.Enemy;
using SpaceInvaders.Core.Features.Enemy.Interfaces;
using SpaceInvaders.Core.Features.Game;
using SpaceInvaders.Core.Features.Player;
using SpaceInvaders.Core.Infrastructure;
using SpaceInvaders.Features.Enemy;
using SpaceInvaders.Framework.Infrastructure;

namespace SpaceInvaders.Infrastructure;

[ServiceProvider]
[Transient<PlayerController>]
[Transient<ProjectileController>]
[Transient<EnemyGridController>]
[Transient<EnemyController>]
[Transient<IEnemyFactory, EnemyFactory>]
[Transient<IEnemyDataReader, EnemyResourceDataReader>]
[Transient<IEnemyViewFactory, EnemyViewFactory>]
[Singleton<IServiceLocator>(Factory = nameof(GetServiceLocator))]
[Singleton<INavigation, Navigation>]
[Singleton<GlobalEventBus>]
[Transient<GameController>]
[Transient<App>(Factory = nameof(GetApp))]
public partial class ServiceLocator : IServiceLocator
{
    public static ServiceLocator Instance { get; } = new();

    public TService Get<TService>()
    {
        return GetService<TService>();
    }

    public IServiceLocator GetServiceLocator()
    {
        return Instance;
    }
    
    public App GetApp()
    {
        return App.Instance;
    }
}