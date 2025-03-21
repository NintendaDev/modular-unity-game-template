using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using Modules.Core.Systems;
using Modules.Core.UI;
using Modules.Wallets.UI.Views;
using UnityEngine;

namespace Game.Application.GameHub.UI
{
    public sealed class MainMenuView : EnableDisableBehaviour, IWalletPanelView
    {
        [SerializeField, Required] private UIButton _settingsButton;
        [SerializeField, Required] private UITextButton _loginButton;
        [SerializeField, Required] private UITextButton _levelsButton;
        [SerializeField, Required] private WalletsPanelView _walletsPanelView;
        
        public event Action SettingButtonClicked;

        public event Action LoginClicked;

        public event Action LevelsButtonClicked;

        private void OnEnable()
        {
            _settingsButton.Clicked += OnSettingButtonClick;
            _loginButton.Clicked += OnLoginClicked;
            _levelsButton.Clicked += OnLevelsClick;
        }

        private void OnDisable()
        {
            _settingsButton.Clicked -= OnSettingButtonClick;
            _loginButton.Clicked -= OnLoginClicked;
            _levelsButton.Clicked -= OnLevelsClick;
        }

        public void Link(IEnumerable<WalletView> walletViews) =>
            _walletsPanelView.Link(walletViews);

        public void EnableLoginButton() =>
            _loginButton.Enable();

        public void DisableLoginButton() =>
            _loginButton.Disable();

        private void OnSettingButtonClick() =>
            SettingButtonClicked?.Invoke();

        private void OnLoginClicked() =>
            LoginClicked?.Invoke();

        private void OnLevelsClick() =>
            LevelsButtonClicked?.Invoke();
    }
}
