using SpaceInvaders.Core.Features.Player;
using SpaceInvaders.Core.Utils;

namespace SpaceInvaders.Core.Features.Main;

public interface IMainView : IView
{ 
    IPlayerView Player { get; }
}