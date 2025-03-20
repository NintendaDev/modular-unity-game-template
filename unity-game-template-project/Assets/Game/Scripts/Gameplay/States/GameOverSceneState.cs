using Cysharp.Threading.Tasks;
using Modules.LoadingCurtain;
using System.Collections.Generic;
using Game.Application.Common;
using Game.Application.LevelLoading;
using Game.Gameplay.View.UI;
using Modules.Analytics;
using Modules.AudioManagement.Player;
using Modules.Core.Systems;
using Modules.EventBus;
using Modules.Logging;

namespace Game.Gameplay.States
{
    public sealed class GameOverSceneState : LevelSceneState
    {
        private readonly GameStateMachine _gameStateMachine;

        public GameOverSceneState(GameStateMachine gameStateMachine, SceneStateMachine sceneStateMachine, 
            ILogSystem logSystem, ISignalBus signalBus, IAnalyticsSystem analyticsSystem, 
            IAudioAssetPlayer audioAssetPlayer, IEnumerable<IReset> resetObjects, ILoadingCurtain loadingCurtain, 
            ICurrentLevelConfiguration levelConfigurator)
            : base(sceneStateMachine, signalBus, logSystem, analyticsSystem, audioAssetPlayer, 
                  resetObjects, loadingCurtain, levelConfigurator)
        {
            _gameStateMachine = gameStateMachine;
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            StateSignalBus.Subscribe<UIRestartSignal>(OnUIRestartSignal);
            StateSignalBus.Subscribe<UIExitSignal>(OnUIExitSignal);

            PauseAllSounds();
            StopGameTime();
        }

        public override async UniTask Exit()
        {
            await base.Exit();

            StateSignalBus.Unsubscribe<UIRestartSignal>(OnUIRestartSignal);
            StateSignalBus.Unsubscribe<UIExitSignal>(OnUIExitSignal);
        }

        private async void OnUIRestartSignal()
        {
            RestoreGameTime();
            await Exit();
            await _gameStateMachine.SwitchState<GameplayGameState>();
        }

        private async void OnUIExitSignal() => await SwitchFinishState();
    }
}
