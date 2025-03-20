using System;
using Modules.SaveSystem.Repositories;
using Modules.SaveSystem.Repositories.SerializeStrategies;
using Modules.SaveSystem.SaveStrategies;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Application.Bootstrap
{
    public class RepositoryInstaller : Installer<RepositoryInstaller.RepositoryConfiguration, RepositoryInstaller>
    {
        [Inject]
        private RepositoryConfiguration _configuration;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<JsonSerialization>()
                .AsSingle()
                .WhenInjectedInto<GameRepository>();
            
            Container.BindInterfacesTo<PrefsStorage>()
                .AsSingle()
                .WithArguments(_configuration.PrefsKey)
                .WhenInjectedInto<GameRepository>();

            Container.BindInterfacesTo<GameRepository>()
                .AsSingle()
                .WithArguments(_configuration.AesPassword);
        }

        [Serializable]
        public struct RepositoryConfiguration
        {
            [SerializeField, Required] public string PrefsKey;
            [SerializeField] public string AesPassword;
        }
    }
}