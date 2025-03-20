using Game.Application.Common;
using Game.Gameplay.States;
using Game.UI.Gameplay.PauseMenu.Presenters;
using Game.UI.Gameplay.PauseMenu.Views;
using Game.UI.Gameplay.PlayMenu.Views;
using Game.UI.Gameplay.Presenters;
using Sirenix.OdinInspector;

using UnityEngine;

namespace Game.Infrastructure.ContextInstallers.Gameplay
{
    public sealed class GameplayInfrastructureInstaller : InfrastructureInstaller
    {
        [SerializeField, Required] private PlayMenuView _playMenuView;
        [SerializeField, Required] private PauseMenuView _pauseMenuView;

        public override void InstallBindings()
        {
            base.InstallBindings();

            BindPlayMenuPresenter();
            BindPauseMenuPresenter();
            BindGameplayBootstrapper();
        }

        private void BindPlayMenuPresenter() =>
            Container.BindInterfacesTo<PlayMenuPresenter>()
            .AsSingle()
            .WithArguments(_playMenuView)
            .NonLazy();

        private void BindPauseMenuPresenter() =>
            Container.BindInterfacesTo<PauseMenuPresenter>()
            .AsSingle()
            .WithArguments(_pauseMenuView)
            .NonLazy();

        private void BindGameplayBootstrapper() =>
            Container.BindInterfacesTo<GameplayBootstrapper>().AsSingle().NonLazy();
    }
}
