using Zenject;

namespace Game.Application.Bootstrap
{
    public class GameBootstrapInstaller : Installer<GameBootstrapInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameBootstrapperFactory>().AsSingle();
        }
    }
}