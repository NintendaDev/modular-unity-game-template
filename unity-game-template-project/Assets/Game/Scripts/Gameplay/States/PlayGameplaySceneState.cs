using Cysharp.Threading.Tasks;
using Modules.LoadingCurtain;
using System.Collections.Generic;
using Game.Application.Common;
using Game.Application.LevelLoading;
using Game.UI.Gameplay.Signals;
using Modules.Analytics;
using Modules.AudioManagement.Player;
using Modules.AudioManagement.Types;
using Modules.Core.Systems;
using Modules.EventBus;
using Modules.Logging;

namespace Game.Gameplay.States
{
    public sealed class PlayGameplaySceneState : LevelGameplayState
    {
        public PlayGameplaySceneState(SceneStateMachine stateMachine, ISignalBus signalBus,
            ILogSystem logSystem, IAnalyticsSystem analyticsSystem, IAudioAssetPlayer audioAssetPlayer, 
            IEnumerable<IReset> resetObjects, ILoadingCurtain loadingCurtain, 
            ICurrentLevelConfiguration levelConfigurator)
            : base(stateMachine, signalBus, logSystem, analyticsSystem, audioAssetPlayer, resetObjects, 
                  loadingCurtain, levelConfigurator)
        {
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            StateSignalBus.Subscribe<PauseSignal>(OnPauseSignal);

            RestoreGameTime();
            UnpauseAllSounds();
            PlayOrUnpauseSound(AudioCode.LevelMusic);
            HideCurtain();
        }

        public override async UniTask Exit()
        {
            await base.Exit();

            StateSignalBus.Unsubscribe<PauseSignal>(OnPauseSignal);
        }

        private async void OnPauseSignal() =>
            await SwitchPauseState();
    }
}
