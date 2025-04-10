using Cysharp.Threading.Tasks;
using Modules.LoadingCurtain;
using System.Collections.Generic;
using Game.Application.Analytics;
using Game.Application.Common;
using Game.Application.LevelLoading;
using Game.Gameplay.View.UI;
using Modules.AudioManagement.Player;
using Modules.Core.Systems;
using Modules.EventBus;
using Modules.Logging;

namespace Game.Gameplay.States
{
    public sealed class PauseSceneState : LevelSceneState
    {
        public PauseSceneState(SceneStateMachine stateMachine, ILogSystem logSystem,
            ISignalBus signalBus, TemplateAnalyticsSystem analyticsSystem, IAudioAssetPlayer audioAssetPlayer, 
            IEnumerable<IReset> resetObjects, ILoadingCurtain loadingCurtain, 
            ICurrentLevelConfiguration levelConfigurator)
            : base(stateMachine, signalBus, logSystem, analyticsSystem, audioAssetPlayer, 
                  resetObjects, loadingCurtain, levelConfigurator)
        {
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            StateSignalBus.Subscribe<UIPlaySignal>(OnUIPlaySignal);
            StateSignalBus.Subscribe<UIExitSignal>(OnUIExitSignal);

            PauseAllSounds();
            StopGameTime();
        }

        public override async UniTask Exit()
        {
            await base.Exit();

            StateSignalBus.Unsubscribe<UIPlaySignal>(OnUIPlaySignal);
            StateSignalBus.Unsubscribe<UIExitSignal>(OnUIExitSignal);
        }

        private async void OnUIPlaySignal() => await SwitchPlayState();

        private async void OnUIExitSignal() =>
            await SwitchFinishState();
    }
}
