using Game.Application.Common;
using Game.Gameplay.Content;
using Game.Gameplay.States;
using GameTemplate.Systems.Input;
using GameTemplate.Gameplay.Content;
using Modules.Entities;
using Modules.SimplePlatformer.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.GameContext
{
    public sealed class GameContextInstaller : MonoInstaller
    {
        [SerializeField, Required, SceneObjectsOnly] 
        private Entity _character;

        public override void InstallBindings()
        {
            BaseInfrastructureInstaller.Install(Container);
            
            InstallPlayerProvider();
            InstallInputController();
            InstallGameplayBootstrapper();
        }

        private void InstallPlayerProvider()
        {
            Container.Bind<PlayerProvider>()
                .AsSingle()
                .WithArguments(_character);
        }

        private void InstallInputController()
        {
            Container.BindInterfacesTo<PlayerInput>().AsSingle();
            
            Container.BindInterfacesTo<PlayerInputController>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallGameplayBootstrapper() =>
            Container.BindInterfacesTo<GameplayBootstrapper>().AsSingle().NonLazy();
    }
}