using System;
using Modules.EventBus;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class PlayMenuPresenter : IDisposable
    {
        private readonly PlayMenuView _view;
        private readonly ISignalBus _signalBus;

        public PlayMenuPresenter(PlayMenuView view, ISignalBus signalBus)
        {
            _view = view;
            _signalBus = signalBus;

            _view.PauseButtonClicked += OnPauseButtonClick;
        }

        public void Dispose()
        {
            _view.PauseButtonClicked -= OnPauseButtonClick;
        }

        private void OnPauseButtonClick() =>
            _signalBus.Invoke<UIPauseSignal>();
    }
}
