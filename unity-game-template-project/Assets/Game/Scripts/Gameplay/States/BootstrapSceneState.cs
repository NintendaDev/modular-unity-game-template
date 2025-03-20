using Cysharp.Threading.Tasks;
using Game.Application.Common;
using Modules.AudioManagement.Player;
using Modules.AudioManagement.Types;
using Modules.LoadingCurtain;
using Modules.EventBus;
using Modules.Logging;

namespace Game.Gameplay.States
{
    public sealed class BootstrapSceneState : SceneState
    {
        private readonly IAudioAssetPlayer _audioAssetPlayer;
        private readonly ILoadingCurtain _loadingCurtain;

        public BootstrapSceneState(SceneStateMachine stateMachine, ISignalBus signalBus, ILogSystem logSystem, 
            ILoadingCurtain loadingCurtain, IAudioAssetPlayer audioAssetPlayer)
            : base(stateMachine, signalBus, logSystem)
        {
            _loadingCurtain = loadingCurtain;
            _audioAssetPlayer = audioAssetPlayer;
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            _loadingCurtain.ShowWithoutProgressBar();
            await InitializeAsync();

            await StateMachine.SwitchState<StartGameplaySceneState>();
        }

        private async UniTask InitializeAsync()
        {
            await InitializeAudioAssetPlayerAsync();
        }

        private async UniTask InitializeAudioAssetPlayerAsync()
        {
            _audioAssetPlayer.Initialize();
            await _audioAssetPlayer.WarmupAsync(AudioCode.LevelMusic);
        }
    }
}
