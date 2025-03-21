using System;
using Cysharp.Threading.Tasks;
using Modules.AssetsManagement.AddressablesOperations;
using Modules.ObjectsManagement.Factories;
using Zenject;

namespace Game.Application.Bootstrap
{
    public sealed class GameBootstrapperFactory : IDisposable
    {
        private readonly BootstrapAssetAddresser _bootstrapAssetAddresser;
        private readonly PrefabFactoryAsync<GameBootstrapper> _prefabFactory;

        public GameBootstrapperFactory(IInstantiator instantiator, IAddressablesService addressablesService) 
        {
             _bootstrapAssetAddresser = new BootstrapAssetAddresser();
             _prefabFactory = new PrefabFactoryAsync<GameBootstrapper>(instantiator, addressablesService);
        }

        public void Dispose()
        {
            _prefabFactory.Dispose();
        }

        public async UniTask<GameBootstrapper> CreateAsync() =>
            await _prefabFactory.CreateAsync(_bootstrapAssetAddresser.BootstrapPrefabAddress);
    }
}