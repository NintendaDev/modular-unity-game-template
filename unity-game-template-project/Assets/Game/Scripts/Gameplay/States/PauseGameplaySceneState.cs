using Cysharp.Threading.Tasks;
using Modules.LoadingCurtain;
using System.Collections.Generic;
using Game.Application.Common;
using Game.Application.LevelLoading;
using Game.UI.Gameplay.Signals;
using Modules.Analytics;
using Modules.AudioManagement.Player;
using Modules.Core.Systems;
using Modules.EventBus;
using Modules.Logging;

namespace Game.Gameplay.States
{
    public sealed class PauseGameplaySceneState : LevelGameplayState
    {
        public PauseGameplaySceneState(SceneStateMachine stateMachine, ILogSystem logSystem,
            ISignalBus signalBus, IAnalyticsSystem analyticsSystem, IAudioAssetPlayer audioAssetPlayer, 
            IEnumerable<IReset> resetObjects, ILoadingCurtain loadingCurtain, 
            ICurrentLevelConfiguration levelConfigurator)
            : base(stateMachine, signalBus, logSystem, analyticsSystem, audioAssetPlayer, 
                  resetObjects, loadingCurtain, levelConfigurator)
        {
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            StateSignalBus.Subscribe<PlaySignal>(OnPlaySignal);
            StateSignalBus.Subscribe<ExitSignal>(OnExitSignal);

            PauseAllSounds();
            StopGameTime();
        }

        public override async UniTask Exit()
        {
            await base.Exit();

            StateSignalBus.Unsubscribe<PlaySignal>(OnPlaySignal);
            StateSignalBus.Unsubscribe<ExitSignal>(OnExitSignal);
        }

        private async void OnPlaySignal() =>
           await SwitchPlayState();

        private async void OnExitSignal() =>
            await SwitchFinishState();
    }
}
