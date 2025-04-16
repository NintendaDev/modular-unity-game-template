using Sirenix.OdinInspector;
using System;
using Modules.Core.Systems;
using Modules.Core.UI;
using UnityEngine;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class GameOverMenuView : EnableDisableBehaviour
    {
        [SerializeField, Required] private UIButton _exitButton;
        [SerializeField, Required] private UIButton _restartButton;

        public event Action RestartButtonClicked;
        
        public event Action ExitButtonClicked;

        private void OnEnable()
        {
            _restartButton.Clicked += OnRestartButtonClick;
            _exitButton.Clicked += OnExitButtonClick;
        }

        private void OnDisable()
        {
            _restartButton.Clicked -= OnRestartButtonClick;
            _exitButton.Clicked -= OnExitButtonClick;
        }

        private void OnRestartButtonClick() => RestartButtonClicked?.Invoke();

        private void OnExitButtonClick() => ExitButtonClicked?.Invoke();
    }
}
