using System;
using Cysharp.Threading.Tasks;
using Modules.AssetsManagement.AddressablesOperations;
using Modules.ObjectsManagement.Pools;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Modules.ObjectsManagement.Factories
{
    public abstract class PooledFactoryAsync<TObject> : IDisposable
        where TObject : Component
    {
        private readonly AssetReference _prefabReference;
        private readonly IAddressablesService _addressablesService;
        private bool _isInitialize;

        protected PooledFactoryAsync(AssetReference prefabReference, IAddressablesService addressablesService,
            Pool<TObject> pool)
        {
            _prefabReference = prefabReference;
            _addressablesService = addressablesService;
            Pool = pool;
        }

        protected Pool<TObject> Pool { get; private set; }

        public virtual void Dispose()
        {
            if (_isInitialize == false)
                return;
            
            _addressablesService.Release(_prefabReference);
        }
        
        public async UniTask InitializeAsync()
        {
            if (_isInitialize)
                return;
            
            TObject prefab = await _addressablesService
                .LoadByAddressAsync<TObject>(_prefabReference.AssetGUID);
            
            Pool = new Pool<TObject>(prefab);
            _isInitialize = true;
        }
    }
}