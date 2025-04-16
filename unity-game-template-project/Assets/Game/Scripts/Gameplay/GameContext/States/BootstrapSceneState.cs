using Cysharp.Threading.Tasks;
using Game.Application.Analytics;
using Game.Application.Common;
using Game.Application.LevelLoading;
using Modules.AudioManagement.Player;
using Modules.AudioManagement.Types;
using Modules.LoadingCurtain;
using Modules.EventBus;
using Modules.Logging;
using Modules.SimplePlatformer.Input;

namespace Game.Gameplay.GameContext
{
    public sealed class BootstrapSceneState : SceneState
    {
        private readonly IAudioAssetPlayer _audioAssetPlayer;
        private readonly IPlayerInput _playerInput;
        private readonly TemplateAnalyticsSystem _analyticsSystem;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly LevelCode _currentLevelCode;

        public BootstrapSceneState(SceneStateMachine stateMachine, ISignalBus signalBus, ILogSystem logSystem, 
            ILoadingCurtain loadingCurtain, IAudioAssetPlayer audioAssetPlayer, IPlayerInput playerInput,
            TemplateAnalyticsSystem analyticsSystem, ICurrentLevelConfiguration currentLevelConfiguration)
            : base(stateMachine, signalBus, logSystem)
        {
            _loadingCurtain = loadingCurtain;
            _audioAssetPlayer = audioAssetPlayer;
            _playerInput = playerInput;
            _analyticsSystem = analyticsSystem;
            _currentLevelCode = currentLevelConfiguration.CurrentLevelConfiguration.LevelCode;
        }

        public override async UniTask Enter()
        {
            _analyticsSystem.SendLevelLoadEvent(LevelBootStage.GameBootstrap, _currentLevelCode);
            
            await base.Enter();

            _loadingCurtain.ShowWithoutProgressBar();
            await InitializeAsync();

            await StateMachine.SwitchState<StartSceneState>();
        }

        private async UniTask InitializeAsync()
        {
            await _audioAssetPlayer.InitializeAsync();

            await UniTask.WhenAll(_audioAssetPlayer.WarmupAsync(AudioCode.LevelMusic, AudioCode.Jump, 
                    AudioCode.Push, AudioCode.Toss, AudioCode.Lava, AudioCode.Trampoline),
                _playerInput.InitializeAsync());
        }
    }
}
