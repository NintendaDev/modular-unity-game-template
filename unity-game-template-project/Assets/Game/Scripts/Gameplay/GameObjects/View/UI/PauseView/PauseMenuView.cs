using Sirenix.OdinInspector;
using System;
using Modules.Core.UI;
using UnityEngine;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class PauseMenuView : ViewWithBackButton
    {
        [SerializeField, Required] private UIButton _exitButton;

        public event Action ExitButtonClicked;

        protected override void OnEnable()
        {
            base.OnEnable();

            _exitButton.Clicked += OnExitButtonClick;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _exitButton.Clicked -= OnExitButtonClick;
        }

        private void OnExitButtonClick() =>
            ExitButtonClicked?.Invoke();
    }
}
