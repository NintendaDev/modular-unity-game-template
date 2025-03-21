using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Modules.Advertisements.Types;
using Modules.Analytics.Types;
using Modules.Logging;
using Sirenix.Utilities;

namespace Modules.Analytics
{
    public sealed class ParallelAnalyticsSystem : IAnalyticsSystem, IAdRevenueAnalytics
    {
        private readonly IAnalyticsSystem[] _analyticsSystems;

        public ParallelAnalyticsSystem(params IAnalyticsSystem[] analyticsSystems)
        {
            _analyticsSystems = analyticsSystems;
        }
        
        public async UniTask InitializeAsync()
        {
            UniTask[] tasks = _analyticsSystems.Select(x => x.InitializeAsync()).ToArray();
            await UniTask.WhenAll(tasks);
        }

        public bool CanSendEvent(AnalyticsEventCode eventCode) => 
            _analyticsSystems.Any(x => x.CanSendEvent(eventCode));

        public void SendCustomEvent(string eventName) => 
            _analyticsSystems.ForEach(x => x.SendCustomEvent(eventName));

        public void SendCustomEvent(string eventName, Dictionary<string, object> data) => 
            _analyticsSystems.ForEach(x => x.SendCustomEvent(eventName, data));

        public void SendCustomEvent(string eventName, float value) => 
            _analyticsSystems.ForEach(x => x.SendCustomEvent(eventName, value));

        public void SendCustomEvent(AnalyticsEventCode eventCode)
        {
            _analyticsSystems.ForEach(x => x.SendCustomEvent(eventCode));
        }

        public void SendCustomEvent(AnalyticsEventCode eventCode, Dictionary<string, object> data)
        {
            _analyticsSystems.ForEach(x => x.SendCustomEvent(eventCode, data));
        }

        public void SendCustomEvent(AnalyticsEventCode eventCode, float value)
        {
            _analyticsSystems.ForEach(x => x.SendCustomEvent(eventCode, value));
        }

        public void SendInterstitialEvent(AdvertisementAction advertisementAction, AdvertisementPlacement placement,
            AdvertisementsSystemType systemType)
        {
            _analyticsSystems.ForEach(x => 
                x.SendInterstitialEvent(advertisementAction, placement, systemType));
        }

        public void SendRewardEvent(AdvertisementAction advertisementAction, AdvertisementPlacement placement,
            AdvertisementsSystemType systemType)
        {
            _analyticsSystems.ForEach(x => 
                x.SendRewardEvent(advertisementAction, placement, systemType));
        }

        public void SendErrorEvent(LogLevel logLevel, string message)
        {
            _analyticsSystems.ForEach(x => x.SendErrorEvent(logLevel, message));
        }

        public void SendProgressEvent(ProgressStatus progressStatus, string levelName, int progressPercent)
        {
            _analyticsSystems.ForEach(x =>
                x.SendProgressEvent(progressStatus, levelName, progressPercent));
        }

        public void SendProgressEvent(ProgressStatus progressStatus, string levelType, string levelName, 
            int progressPercent)
        {
            _analyticsSystems.ForEach(x =>
                x.SendProgressEvent(progressStatus, levelType, levelName, progressPercent));
        }

        public void SendAdvertisementRevenueEvent(AdvertisementRevenue revenue)
        {
            foreach (IAnalyticsSystem analyticsSystem in _analyticsSystems)
            {
                if (analyticsSystem is IAdRevenueAnalytics adRevenueAnalytics)
                    adRevenueAnalytics.SendAdvertisementRevenueEvent(revenue);
            }
        }
    }
}