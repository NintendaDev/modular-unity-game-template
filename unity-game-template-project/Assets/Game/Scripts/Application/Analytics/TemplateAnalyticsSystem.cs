using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Application.Common;
using Modules.Advertisements.Types;
using Modules.Analytics;
using Modules.Analytics.Configurations;
using Modules.Analytics.Types;
using Modules.AssetsManagement.StaticData;
using Modules.Logging;
using Modules.Text;

namespace Game.Application.Analytics
{
    public sealed class TemplateAnalyticsSystem : IAnalyticsSystem
    {
        private readonly IAnalyticsSystem _analyticsSystem;
        private readonly IStaticDataService _staticDataService;
        private readonly List<GameBootStage> _sendedGameBootEvents = new();
        private AnalyticsParamsNames _parametersNames;
        private AnalyticsEventsNames _eventsNames;

        public TemplateAnalyticsSystem(IAnalyticsSystem analyticsSystem, IStaticDataService staticDataService)
        {
            _analyticsSystem = analyticsSystem;
            _staticDataService = staticDataService;
        }

        public async UniTask InitializeAsync()
        {
            await _analyticsSystem.InitializeAsync();
            _parametersNames = _staticDataService.GetConfiguration<AnalyticsParamsNames>();
            _eventsNames = _staticDataService.GetConfiguration<AnalyticsEventsNames>();
        }

        public bool CanSendEvent(AnalyticsEventCode eventCode) => _analyticsSystem.CanSendEvent(eventCode);

        public void SendCustomEvent(string eventName) => _analyticsSystem.SendCustomEvent(eventName);

        public void SendCustomEvent(string eventName, Dictionary<string, object> data) => 
            _analyticsSystem.SendCustomEvent(eventName, data);

        public void SendCustomEvent(string eventName, float value) => 
            _analyticsSystem.SendCustomEvent(eventName, value);

        public void SendCustomEvent(AnalyticsEventCode eventCode) => 
            _analyticsSystem.SendCustomEvent(eventCode);

        public void SendCustomEvent(AnalyticsEventCode eventCode, Dictionary<string, object> data) => 
            _analyticsSystem.SendCustomEvent(eventCode, data);

        public void SendCustomEvent(AnalyticsEventCode eventCode, float value) => 
            _analyticsSystem.SendCustomEvent(eventCode, value);

        public void SendInterstitialEvent(AdvertisementAction advertisementAction, AdvertisementPlacement placement,
            AdvertisementsSystemType systemType)
        {
            _analyticsSystem.SendInterstitialEvent(advertisementAction, placement, systemType);
        }

        public void SendRewardEvent(AdvertisementAction advertisementAction, AdvertisementPlacement placement,
            AdvertisementsSystemType systemType)
        {
            _analyticsSystem.SendInterstitialEvent(advertisementAction, placement, systemType);
        }

        public void SendErrorEvent(LogLevel logLevel, string message) => 
            _analyticsSystem.SendErrorEvent(logLevel, message);

        public void SendProgressEvent(ProgressStatus progressStatus, string levelName, int progressPercent) =>
            _analyticsSystem.SendProgressEvent(progressStatus, levelName, progressPercent);

        public void SendProgressEvent(ProgressStatus progressStatus, string levelType, string levelName, 
            int progressPercent)
        {
            _analyticsSystem.SendProgressEvent(progressStatus, levelType, levelName, progressPercent);
        }
        
        public void SendGameBootEvent(GameBootStage stage)
        {
            if (_sendedGameBootEvents.Contains(stage))
                return;

            Dictionary<string, object> parameters = new()
            {
                { _parametersNames.GameBootStage, stage.ToString().ToSnakeCase() },
            };
            
            _analyticsSystem.SendCustomEvent(_eventsNames.GameBoot, parameters);
            _sendedGameBootEvents.Add(stage);
        }
        
        public void SendLevelLoadEvent(LevelBootStage bootStage, LevelCode levelCode)
        {
            Dictionary<string, object> parameters = new()
            {
                { _parametersNames.LevelBootStage, bootStage.ToString().ToSnakeCase() },
                { _parametersNames.LevelCode, levelCode.ToString().ToSnakeCase() },
            };
            
            _analyticsSystem.SendCustomEvent(_eventsNames.LevelBoot, parameters);
        }
        
        public void SendLevelProgressEvent(LevelCode levelCode, int progressPercent)
        {
            Dictionary<string, object> parameters = new()
            {
                { _parametersNames.LevelCode, levelCode.ToString().ToSnakeCase() },
                { _parametersNames.ProgressPercent, progressPercent },
            };
            
            _analyticsSystem.SendCustomEvent(_eventsNames.LevelProgress, parameters);
        }
    }
}