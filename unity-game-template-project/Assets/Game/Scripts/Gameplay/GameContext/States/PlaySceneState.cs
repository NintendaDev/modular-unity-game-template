using Cysharp.Threading.Tasks;
using Modules.LoadingCurtain;
using System.Collections.Generic;
using Game.Application.Analytics;
using Game.Application.Common;
using Game.Application.LevelLoading;
using GameTemplate.Gameplay.GameObjects;
using Modules.AudioManagement.Player;
using Modules.AudioManagement.Types;
using Modules.Core.Systems;
using Modules.EventBus;
using Modules.Logging;

namespace Game.Gameplay.GameContext
{
    public sealed class PlaySceneState : LevelSceneState
    {
        public PlaySceneState(SceneStateMachine stateMachine, ISignalBus signalBus,
            ILogSystem logSystem, TemplateAnalyticsSystem analyticsSystem, IAudioAssetPlayer audioAssetPlayer, 
            IEnumerable<IReset> resetObjects, ILoadingCurtain loadingCurtain, 
            ICurrentLevelConfiguration levelConfigurator)
            : base(stateMachine, signalBus, logSystem, analyticsSystem, audioAssetPlayer, resetObjects, 
                  loadingCurtain, levelConfigurator)
        {
        }

        public override async UniTask Enter()
        {
            AnalyticsSystem.SendLevelLoadEvent(LevelBootStage.Complete, CurrentLevelConfiguration.LevelCode);
            
            await base.Enter();

            StateSignalBus.Subscribe<UIPauseSignal>(OnUIPauseSignal);
            StateSignalBus.Subscribe<PlayerDieSignal>(OnPlayerDie);
            StateSignalBus.Subscribe<PlayerWinSignal>(OnPlayerWin);

            RestoreGameTime();
            UnpauseAllSounds();
            PlayOrUnpauseSound(AudioCode.LevelMusic);
            HideCurtain();
        }

        public override async UniTask Exit()
        {
            await base.Exit();

            StateSignalBus.Unsubscribe<UIPauseSignal>(OnUIPauseSignal);
            StateSignalBus.Unsubscribe<PlayerDieSignal>(OnPlayerDie);
            StateSignalBus.Unsubscribe<PlayerWinSignal>(OnPlayerWin);
        }

        private async void OnUIPauseSignal() => await SwitchPauseState();

        private async void OnPlayerDie() => await SwitchGameOverState();

        private async void OnPlayerWin() => await SwitchGameOverState();
    }
}
