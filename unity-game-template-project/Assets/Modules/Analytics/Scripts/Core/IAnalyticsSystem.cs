using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Modules.Advertisements.Types;
using Modules.Analytics.Types;
using Modules.Logging;

namespace Modules.Analytics
{
    public interface IAnalyticsSystem
    {
        public UniTask InitializeAsync();

        public bool CanSendEvent(AnalyticsEventCode eventCode);

        public void SendCustomEvent(string eventName);
        
        public void SendCustomEvent(string eventName, Dictionary<string, object> data);
        
        public void SendCustomEvent(string eventName, float value);

        public void SendCustomEvent(AnalyticsEventCode eventCode);

        public void SendCustomEvent(AnalyticsEventCode eventCode, Dictionary<string, object> data);
        
        public void SendCustomEvent(AnalyticsEventCode eventCode, float value);
        
        public void SendInterstitialEvent(AdvertisementAction advertisementAction,
            AdvertisementPlacement placement, AdvertisementsSystemType systemType);

        public void SendRewardEvent(AdvertisementAction advertisementAction, AdvertisementPlacement placement,
            AdvertisementsSystemType systemType);

        public void SendErrorEvent(LogLevel logLevel, string message);

        public void SendProgressEvent(ProgressStatus progressStatus, string levelName, int progressPercent);

        public void SendProgressEvent(ProgressStatus progressStatus, string levelType, string levelName, 
            int progressPercent);
    }
}