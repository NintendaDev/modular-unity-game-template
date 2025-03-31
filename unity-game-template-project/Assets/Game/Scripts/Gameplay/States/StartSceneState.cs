using Cysharp.Threading.Tasks;
using Modules.LoadingCurtain;
using System.Collections.Generic;
using Game.Application.Advertisements;
using Game.Application.Common;
using Game.Application.LevelLoading;
using Modules.Advertisements.Types;
using Modules.Analytics;
using Modules.AudioManagement.Player;
using Modules.Core.Systems;
using Modules.EventBus;
using Modules.Logging;

namespace Game.Gameplay.States
{
    public sealed class StartSceneState : LevelSceneState
    {
        private readonly AdvertisementsFacade _advertisementsFacade;

        public StartSceneState(SceneStateMachine stateMachine, ILogSystem logSystem,
            ISignalBus signalBus, IAnalyticsSystem analyticsSystem, IAudioAssetPlayer audioAssetPlayer, 
            IEnumerable<IReset> resetObjects, ILoadingCurtain loadingCurtain, 
            ICurrentLevelConfiguration levelConfigurator, AdvertisementsFacade _advertisementsFacade)
            : base(stateMachine, signalBus, logSystem, analyticsSystem, audioAssetPlayer, resetObjects, 
                  loadingCurtain, levelConfigurator)
        {
            this._advertisementsFacade = _advertisementsFacade;
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            ShowCurtain();
            ResetGameplay();

            if (_advertisementsFacade.TryShowInterstitial(AdvertisementPlacement.StartLevel,
                    onCloseCallback: OnInterstitialFinished) == false)
            {
                await SwitchNextState();
            }
        }

        private async void OnInterstitialFinished() =>
            await SwitchNextState();

        private async UniTask SwitchNextState() =>
            await SwitchPlayState();
    }
}