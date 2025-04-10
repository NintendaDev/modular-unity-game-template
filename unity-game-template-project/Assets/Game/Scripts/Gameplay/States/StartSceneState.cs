using Cysharp.Threading.Tasks;
using Modules.LoadingCurtain;
using System.Collections.Generic;
using Game.Application.Advertisements;
using Game.Application.Analytics;
using Game.Application.Common;
using Game.Application.LevelLoading;
using Modules.Advertisements.Types;
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
            ISignalBus signalBus, TemplateAnalyticsSystem analyticsSystem, IAudioAssetPlayer audioAssetPlayer, 
            IEnumerable<IReset> resetObjects, ILoadingCurtain loadingCurtain, 
            ICurrentLevelConfiguration levelConfigurator, AdvertisementsFacade advertisementsFacade)
            : base(stateMachine, signalBus, logSystem, analyticsSystem, audioAssetPlayer, resetObjects, 
                  loadingCurtain, levelConfigurator)
        {
            _advertisementsFacade = advertisementsFacade;
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            ShowCurtain();
            ResetGameplay();
            
            AnalyticsSystem.SendLevelLoadEvent(LevelBootStage.ShowAdvertisement, CurrentLevelConfiguration.LevelCode);

            if (_advertisementsFacade.TryShowInterstitial(AdvertisementPlacement.StartLevel,
                    onCloseCallback: OnInterstitialFinished) == false)
            {
                await SwitchNextState();
            }
        }

        private async void OnInterstitialFinished() => await SwitchNextState();

        private async UniTask SwitchNextState() => await SwitchPlayState();
    }
}