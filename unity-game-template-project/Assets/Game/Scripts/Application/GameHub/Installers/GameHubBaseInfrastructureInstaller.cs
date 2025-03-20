using Game.Application.Common;
using Game.Application.GameHub;
using Game.Application.GameHub.UI;
using Modules.AudioManagement.UI.Presenters;
using Modules.AudioManagement.UI.Views;
using Modules.Wallets.UI.Factories;
using Modules.Wallets.UI.Presenters;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Application.HameHub.Installers
{
    public sealed class GameHubBaseInfrastructureInstaller : MonoInstaller
    {
        [SerializeField, Required] private MainMenuView _mainMenuView;
        [SerializeField, Required] private LevelsMenuView _levelsMenuView;
        [SerializeField, Required] private AudioSettingsView _audioSettingsView;

        public override void InstallBindings()
        {
            BaseInfrastructureInstaller.Install(Container);

            BindMainMenuPresenter();
            BindLevelsMenuPresenter();
            BindWalletsPanelPresenter();
            BindSettingsPresenter();
            BindGameHubBootstrapper();
        }

        private void BindMainMenuPresenter() 
        {
            Container.BindInterfacesAndSelfTo<MainMenuPresenter>()
                .AsSingle()
                .WithArguments(_mainMenuView)
                .NonLazy();
        }

        private void BindLevelsMenuPresenter()
        {
            Container.BindInterfacesAndSelfTo<LevelViewFactory>()
                .AsSingle()
                .WhenInjectedInto<LevelsMenuPresenter>();

            Container.Bind<LevelsMenuPresenter>()
                .AsSingle()
                .WithArguments(_levelsMenuView)
                .NonLazy();
        }
            
        private void BindWalletsPanelPresenter()
        {
            Container.BindInterfacesAndSelfTo<WalletViewFactory>()
                .AsSingle()
                .WhenInjectedInto<WalletsPanelPresenter>();

            Container.Bind<WalletsPanelPresenter>()
                .AsSingle()
                .WithArguments(_mainMenuView)
                .NonLazy();
        }
            
        private void BindSettingsPresenter()
        {
            Container.BindInterfacesTo<AudioSettingsPresenter>()
                .AsSingle()
                .WithArguments(_audioSettingsView)
                .NonLazy();
        }
            
        private void BindGameHubBootstrapper() =>
             Container.BindInterfacesTo<GameHubBootstrapper>().AsSingle().NonLazy();
    }
}
