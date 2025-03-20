using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Gameplay.View.UI
{
    public sealed class GameplayUIInstaller : MonoInstaller
    {
        [SerializeField, Required] private PlayMenuView _playMenuViewView;
        [SerializeField, Required] private PauseMenuView _pauseMenuViewView;
        [SerializeField, Required] private GameOverMenuView _gameOverMenuView;

        public override void InstallBindings()
        {
            BindPlayMenuPresenter();
            BindPauseMenuPresenter();
            BindgameoverMenuPresenter();
        }

        private void BindPlayMenuPresenter() =>
            Container.BindInterfacesTo<PlayMenuPresenter>()
            .AsSingle()
            .WithArguments(_playMenuViewView)
            .NonLazy();

        private void BindPauseMenuPresenter() =>
            Container.BindInterfacesTo<PauseMenuPresenter>()
            .AsSingle()
            .WithArguments(_pauseMenuViewView)
            .NonLazy();
        
        private void BindgameoverMenuPresenter() =>
            Container.BindInterfacesTo<GameOverMenuPresenter>()
                .AsSingle()
                .WithArguments(_gameOverMenuView)
                .NonLazy();
    }
}
