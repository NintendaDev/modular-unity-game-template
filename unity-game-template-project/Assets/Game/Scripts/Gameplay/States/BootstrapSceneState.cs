using Cysharp.Threading.Tasks;
using Game.Application.Common;
using Modules.AudioManagement.Player;
using Modules.AudioManagement.Types;
using Modules.LoadingCurtain;
using Modules.EventBus;
using Modules.Logging;
using Modules.SimplePlatformer.Input;

namespace Game.Gameplay.States
{
    public sealed class BootstrapSceneState : SceneState
    {
        private readonly IAudioAssetPlayer _audioAssetPlayer;
        private readonly IPlayerInput _playerInput;
        private readonly ILoadingCurtain _loadingCurtain;

        public BootstrapSceneState(SceneStateMachine stateMachine, ISignalBus signalBus, ILogSystem logSystem, 
            ILoadingCurtain loadingCurtain, IAudioAssetPlayer audioAssetPlayer, IPlayerInput playerInput)
            : base(stateMachine, signalBus, logSystem)
        {
            _loadingCurtain = loadingCurtain;
            _audioAssetPlayer = audioAssetPlayer;
            _playerInput = playerInput;
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            _loadingCurtain.ShowWithoutProgressBar();
            await InitializeAsync();

            await StateMachine.SwitchState<StartSceneState>();
        }

        private async UniTask InitializeAsync()
        {
            await _audioAssetPlayer.InitializeAsync();

            await UniTask.WhenAll(_audioAssetPlayer.WarmupAsync(AudioCode.LevelMusic, AudioCode.Jump, AudioCode.Push,
                    AudioCode.Toss, AudioCode.Lava, AudioCode.Trampoline),
                _playerInput.InitializeAsync());
        }
    }
}
