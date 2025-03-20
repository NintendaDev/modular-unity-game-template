using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using Modules.AssetsManagement.AddressablesOperations;
using UnityEngine;

namespace Modules.AssetsManagement.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAddressablesService _addressablesService;
        private readonly StaticDataServiceConfiguration _serviceConfiguration;
        private readonly Dictionary<Type, UnityEngine.Object> _configurations = new();

        public StaticDataService(IAddressablesService addressablesService, 
            StaticDataServiceConfiguration serviceConfiguration)
        {
            _addressablesService = addressablesService;
            _serviceConfiguration = serviceConfiguration;
        }

        public async UniTask InitializeAsync() => await LoadAllConfigurations();
        
        public TConfigType GetConfiguration<TConfigType>() where TConfigType : ScriptableObject
        {
            Type configurationType = typeof(TConfigType);

            if (_configurations.ContainsKey(configurationType) == false)
                throw new Exception($"Configuration type {configurationType} is not exists");

            return _configurations[configurationType] as TConfigType;
        }

        private async UniTask LoadAllConfigurations()
        {
            ScriptableObject[] rawConfigurations = await _addressablesService
                .LoadByLabelAsync<ScriptableObject>(_serviceConfiguration.ConfigurationsAssetLabel);

            foreach (ScriptableObject rawConfiguration in rawConfigurations)
            {
                _configurations.Add(rawConfiguration.GetType(), rawConfiguration);
            }
        }
    }
}