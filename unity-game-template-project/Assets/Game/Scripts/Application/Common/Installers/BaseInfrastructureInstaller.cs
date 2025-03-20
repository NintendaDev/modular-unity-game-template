using Modules.StateMachines;
using System;
using Modules.AudioManagement.Player;
using Modules.Core.Systems;
using Modules.Localization.Core.Processors;
using Modules.Localization.Core.Processors.Factories;
using Zenject;

namespace Game.Application.Common
{
    public sealed class BaseInfrastructureInstaller : Installer<BaseInfrastructureInstaller>
    {
        public override void InstallBindings()
        {
            BindResetObjects();
            BindAudioAssetPlayer();
            BindLocalizationProcessors();
            BindStateMachine();
            BindSceneObjectsDisposable();
        }

        private void BindResetObjects() =>
            Container.Bind<IReset>().FromComponentsInHierarchy().AsSingle();

        private void BindAudioAssetPlayer() =>
            Container.BindInterfacesTo<AudioAssetPlayer>().AsSingle();

        private void BindLocalizationProcessors()
        {
            Container.BindInterfacesAndSelfTo<LocalizedTermProcessorFactory>()
                .AsSingle()
                .WhenInjectedInto<LocalizedTermProcessorLinker>();
            
            Container.BindInterfacesAndSelfTo<LocalizedTermProcessorLinker>().AsSingle();
        }

        private void BindStateMachine()
        {
            Container.Bind<SceneStateMachine>().AsSingle();
            Container.Bind<StatesFactory>().AsSingle();
        }

        private void BindSceneObjectsDisposable() =>
            Container.Bind<IDisposable>().To<SceneObjectDisposable>()
            .FromComponentInHierarchy().AsSingle();
    }
}
