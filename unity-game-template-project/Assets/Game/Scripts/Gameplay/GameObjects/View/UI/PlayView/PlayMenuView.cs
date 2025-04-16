using Sirenix.OdinInspector;
using System;
using Modules.Core.Systems;
using Modules.Core.UI;
using UnityEngine;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class PlayMenuView : EnableDisableBehaviour
    {
        [SerializeField, Required] private UIButton _pauseButton;

        public event Action PauseButtonClicked;

        private void OnEnable()
        {
            _pauseButton.Clicked += OnPauseButtonClick;
        }

        private void OnDisable()
        {
            _pauseButton.Clicked -= OnPauseButtonClick;
        }

        private void OnPauseButtonClick() =>
            PauseButtonClicked?.Invoke();
    }
}
