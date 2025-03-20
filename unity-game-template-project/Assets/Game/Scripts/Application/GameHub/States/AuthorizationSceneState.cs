using Cysharp.Threading.Tasks;
using Game.Application.Common;
using Game.Application.Loading;
using Modules.AudioManagement.Mixer;
using Modules.Authorization.Interfaces;
using Modules.LoadingCurtain;
using Modules.EventBus;
using Modules.Localization.Core.Types;
using Modules.Logging;
using Modules.PopupsSystem;

namespace Game.Application.GameHub
{
    public sealed class AuthorizationSceneState : SceneState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAuthorizationService _authorizationService;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IPopups _popups;
        private readonly IAudioMixerSystem _audioMixerSystem;

        public AuthorizationSceneState(SceneStateMachine stateMachine, ISignalBus signalBus, 
            GameStateMachine gameStateMachine, ILogSystem logSystem,
            IAuthorizationService authorizationService, ILoadingCurtain loadingCurtain, 
            IAudioMixerSystem audioMixerSystem, IPopups popups) 
            : base(stateMachine, signalBus, logSystem)
        {
            _gameStateMachine = gameStateMachine;
            _authorizationService = authorizationService;
            _loadingCurtain = loadingCurtain;
            _audioMixerSystem = audioMixerSystem;
            _popups = popups;
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            _audioMixerSystem.Mute();
            _loadingCurtain.ShowWithoutProgressBar();

            _authorizationService.LoginCompleted += OnLoginCompleted;
            _authorizationService.LoginError += OnLoginError;

            _authorizationService.StartAuthorizationBehaviour();
        }

        public override async UniTask Exit()
        {
            await base.Exit();

            _authorizationService.LoginCompleted -= OnLoginCompleted;
            _authorizationService.LoginError -= OnLoginError;
        }

        private async void OnLoginCompleted()
        {
            await _popups.ShowInfoAsync(LocalizationTerm.Info, LocalizationTerm.SuccessAuthorizationMessage, 
                LocalizationTerm.Ok);

            base.Exit();
            
            await _gameStateMachine.SwitchState<GameLoadingState>();
        }

        private async void OnLoginError()
        {
            await _popups.ShowErrorAsync(LocalizationTerm.Info, LocalizationTerm.SuccessAuthorizationMessage, 
                LocalizationTerm.Ok);
            
            await StateMachine.SwitchState<MainSceneState>();
        }    
    }
}
