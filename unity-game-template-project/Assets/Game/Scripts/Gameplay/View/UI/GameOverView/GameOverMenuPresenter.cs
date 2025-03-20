using System;
using Modules.EventBus;

namespace Game.Gameplay.View.UI
{
    public sealed class GameOverMenuPresenter : IDisposable
    {
        private readonly GameOverMenuView _menuView;
        private readonly ISignalBus _signalBus;

        public GameOverMenuPresenter(GameOverMenuView menuView, ISignalBus signalBus)
        {
            _menuView = menuView;
            _signalBus = signalBus;
            
            _menuView.RestartButtonClicked += OnRestartButtonClick;
            _menuView.ExitButtonClicked += OnExitButtonClick;
        }

        public void Dispose()
        {
            _menuView.RestartButtonClicked -= OnRestartButtonClick;
            _menuView.ExitButtonClicked -= OnExitButtonClick;
        }

        private void OnRestartButtonClick() => _signalBus.Invoke<UIRestartSignal>();

        private void OnExitButtonClick() => _signalBus.Invoke<UIExitSignal>();
    }
}
