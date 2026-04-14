namespace SpaceInvaders.Core.Infrastructure;

public interface IServiceLocator
{
    TService Get<TService>();
}