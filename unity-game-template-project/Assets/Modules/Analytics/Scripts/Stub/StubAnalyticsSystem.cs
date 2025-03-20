using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Modules.Advertisements.Types;
using Modules.Analytics.Types;
using Modules.AssetsManagement.StaticData;
using Modules.Logging;

namespace Modules.Analytics.Stub
{
    public sealed class StubAnalyticsSystem : AnalyticsSystem, IAdRevenueAnalytics
    {
        public StubAnalyticsSystem(ILogSystem logSystem, IStaticDataService staticDataService) 
            : base(logSystem, staticDataService)
        {
        }

        public async override UniTask InitializeAsync()
        {
            await base.InitializeAsync();
            LogSystem.SetPrefix("[Stub Analytics] ");
        }

        public override void SendCustomEvent(string eventName)
        {
            LogEvent(eventName);
        }

        public override void SendCustomEvent(string eventName, Dictionary<string, object> data)
        {
            LogEvent(eventName, data);
        }

        public override void SendCustomEvent(string eventName, float value)
        {
            LogEvent(eventName, value);
        }

        public override void SendInterstitialEvent(AdvertisementAction advertisementAction, 
            AdvertisementPlacement placement, AdvertisementsSystemType systemType)
        {
            LogEvent(advertisementAction, placement, systemType);
        }

        public override void SendRewardEvent(AdvertisementAction advertisementAction, AdvertisementPlacement placement,
            AdvertisementsSystemType systemType)
        {
            LogEvent(advertisementAction, placement, systemType);
        }

        public override void SendErrorEvent(LogLevel logLevel, string message)
        {
            LogEvent(logLevel, message);
        }

        public override void SendProgressEvent(ProgressStatus progressStatus, string levelName, int progressPercent)
        {
            LogEvent(progressStatus, String.Empty, levelName, progressPercent);
        }

        public override void SendProgressEvent(ProgressStatus progressStatus, string levelType, string levelName, 
            int progressPercent)
        {
            LogEvent(progressStatus, levelType, levelName, progressPercent);
        }

        public void SendAdvertisementRevenueEvent(AdvertisementRevenue revenue)
        {
            LogEvent(revenue);
        }

        protected override void SendCustomEventInternal(AnalyticsEventCode eventCode)
        {
            SendCustomEvent(eventCode.ToString());
        }

        protected override void SendCustomEventInternal(AnalyticsEventCode eventCode, Dictionary<string, object> data)
        {
            SendCustomEvent(eventCode.ToString(), data);
        }

        protected override void SendCustomEventInternal(AnalyticsEventCode eventCode, float value)
        {
            SendCustomEvent(eventCode.ToString(), value);
        }
    }
}
