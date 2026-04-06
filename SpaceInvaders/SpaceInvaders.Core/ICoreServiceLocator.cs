using Jab;
using SpaceInvaders.Core.Features.Main;

namespace SpaceInvaders.Core;

[ServiceProviderModule]
[Transient<GameController>]
public interface ICoreServiceLocator
{
}