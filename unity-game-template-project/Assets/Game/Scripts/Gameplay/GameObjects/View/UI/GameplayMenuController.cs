using Modules.EventBus;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class GameplayMenuController : MonoBehaviour
    {
        [SerializeField, Required] private PlayMenuView _playMenuView;
        [SerializeField, Required] private PauseMenuView _pauseMenuView;
        [SerializeField, Required] private GameOverMenuView _gameOverMenuView;
        
        private ISignalBus _signalBus;

        [Inject]
        private void Construct(ISignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _playMenuView.PauseButtonClicked += OnPauseButtonClick;
            _pauseMenuView.BackButtonClicked += OnPauseMenuViewBackButtonClick;
            _signalBus.Subscribe<PlayerDieSignal>(OnPlayerDie);
            _signalBus.Subscribe<PlayerWinSignal>(OnPlayerWin);
        }

        private void OnDisable()
        {
            _playMenuView.PauseButtonClicked -= OnPauseButtonClick;
            _pauseMenuView.BackButtonClicked -= OnPauseMenuViewBackButtonClick;
            _signalBus.Unsubscribe<PlayerDieSignal>(OnPlayerDie);
            _signalBus.Unsubscribe<PlayerWinSignal>(OnPlayerWin);
        }

        private void Start()
        {
            EnablePlayMenu();
        }

        private void OnPauseButtonClick() =>
            EnablePauseMenu();

        private void EnablePauseMenu()
        {
            DisableAllViews();
            _pauseMenuView.Enable();
        }

        private void DisableAllViews()
        {
            _playMenuView.Disable();
            _pauseMenuView.Disable();
        }

        private void OnPauseMenuViewBackButtonClick() => EnablePlayMenu();

        private void EnablePlayMenu()
        {
            DisableAllViews();
            _playMenuView.Enable();
        }

        private void OnPlayerDie() => EnableGameOverMenu();

        private void EnableGameOverMenu()
        {
            DisableAllViews();
            _gameOverMenuView.Enable();
        }

        private void OnPlayerWin() => EnableGameOverMenu();
    }
}
