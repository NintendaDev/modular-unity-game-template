using Cysharp.Threading.Tasks;
using Game.Application.Common;
using Modules.AudioManagement.Player;
using Modules.AudioManagement.Types;
using Modules.EventBus;
using Modules.Logging;

namespace Game.Application.GameHub
{
    public sealed class BootstrapSceneState : SceneState
    {
        private readonly IAudioAssetPlayer _audioAssetPlayer;

        public BootstrapSceneState(SceneStateMachine stateMachine, ISignalBus signalBus, ILogSystem logSystem, 
            IAudioAssetPlayer audioAssetPlayer)
            : base(stateMachine, signalBus, logSystem)
        {
            _audioAssetPlayer = audioAssetPlayer;
        }

        public async override UniTask Enter()
        {
            await base.Enter();

            await InitializeAudioAssetPlayer();
            await StateMachine.SwitchState<MainSceneState>();
        }

        private async UniTask InitializeAudioAssetPlayer()
        {
            _audioAssetPlayer.Initialize();
            await _audioAssetPlayer.WarmupAsync(AudioCode.GameHubMusic);
        }
    }
}