using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Application.GameHub
{
    public sealed class GameHubView : MonoBehaviour
    {
        [SerializeField, Required] private MainMenuView _mainMenuView;
        [SerializeField, Required] private LevelsMenuView _levelsMenuView;
        [SerializeField, Required] private SettingsView _audioSettingsView;

        private void Start()
        {
            EnableMainMenuView();
        }

        private void OnEnable()
        {
            _mainMenuView.SettingButtonClicked += OnSettingsButtonClick;
            _mainMenuView.LevelsButtonClicked += OnLevelsButtonClick;
            _levelsMenuView.BackButtonClicked += OnLevelsMenuBackClick;
            _audioSettingsView.BackButtonClicked += OnAudioSettingsBackButtonClick;
        }

        private void OnDisable()
        {
            _mainMenuView.SettingButtonClicked -= OnSettingsButtonClick;
            _mainMenuView.LevelsButtonClicked -= OnLevelsButtonClick;
            _levelsMenuView.BackButtonClicked += OnLevelsMenuBackClick;
            _audioSettingsView.BackButtonClicked -= OnAudioSettingsBackButtonClick;
        }

        private void EnableMainMenuView()
        {
            DisableAllViews();
            _mainMenuView.Enable();
        }

        private void DisableAllViews()
        {
            _mainMenuView.Disable();
            _audioSettingsView.Disable();
            _levelsMenuView.Disable();
        }

        private void OnSettingsButtonClick() =>
            EnableSettingsView();

        private void EnableSettingsView()
        {
            DisableAllViews();
            _audioSettingsView.Enable();
        }

        private void OnLevelsButtonClick() =>
            EnableLevelsView();

        private void EnableLevelsView()
        {
            DisableAllViews();
            _levelsMenuView.Enable();
        }

        private void OnLevelsMenuBackClick() =>
            EnableMainMenuView();

        private void OnAudioSettingsBackButtonClick() =>
            EnableMainMenuView();
    }
}
