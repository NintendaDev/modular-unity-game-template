using Modules.AssetsManagement.AddressablesOperations;
using Modules.AssetsManagement.StaticData;
using Zenject;

namespace Game.Application.Bootstrap
{
    public class AssetManagementInstaller 
        : Installer<GameLoadingAssetsConfiguration, StaticDataServiceConfiguration, AssetManagementInstaller>
    {
        [Inject]
        private GameLoadingAssetsConfiguration _gameLoadingAssetsConfiguration;
        
        [Inject]
        private StaticDataServiceConfiguration _staticDataServiceConfiguration;

        public override void InstallBindings()
        {
            BindInfrastructureAssetsConfiguration();
            BindAddressablesServices();
            BindStaticDataService();
        }
        
        private void BindInfrastructureAssetsConfiguration() =>
            Container.Bind<GameLoadingAssetsConfiguration>().FromInstance(_gameLoadingAssetsConfiguration);
        

        private void BindAddressablesServices() => Container.BindInterfacesTo<AddressablesService>().AsSingle();
            
        private void BindStaticDataService() =>
            Container.BindInterfacesAndSelfTo<StaticDataService>()
                .AsSingle()
                .WithArguments(_staticDataServiceConfiguration);
    }
}