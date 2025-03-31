using Game.Application.Advertisements;
using Modules.Advertisements.Dummy;
using Zenject;

namespace Game.Application.Bootstrap
{
    public class AdvertisementsInstaller : Installer<AdvertisementsInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<DummyAdvertisementsSystem>()
                .AsSingle()
                .WhenInjectedInto<AdvertisementsFacade>();
            
            Container.BindInterfacesAndSelfTo<AdvertisementsFacade>().AsSingle();
        }
    }
}