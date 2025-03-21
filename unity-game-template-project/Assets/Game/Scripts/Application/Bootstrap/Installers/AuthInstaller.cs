using Modules.Authorization.Interfaces;
using Modules.NetworkAccount;
using Zenject;

namespace Game.Application.Bootstrap
{
    public class AuthInstaller : Installer<AuthInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<DummyNetworkAccount>().AsSingle();
            Container.BindInterfacesTo<DummyAuthorizationService>().AsSingle();
        }
    }
}