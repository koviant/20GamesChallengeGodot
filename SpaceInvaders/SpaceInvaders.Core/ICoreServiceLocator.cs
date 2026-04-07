using Jab;
using SpaceInvaders.Core.Features.Game;

namespace SpaceInvaders.Core;

[ServiceProviderModule]
[Transient<GameController>]
public interface ICoreServiceLocator
{
}