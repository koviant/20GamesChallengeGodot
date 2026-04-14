namespace SpaceInvaders.Framework.Infrastructure;

public interface IServiceLocator
{
    TService Get<TService>();
}