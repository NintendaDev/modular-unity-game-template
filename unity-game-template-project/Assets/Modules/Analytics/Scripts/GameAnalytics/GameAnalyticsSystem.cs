using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameAnalyticsSDK;
using Modules.Advertisements.Types;
using Modules.Analytics.Types;
using Modules.AssetsManagement.StaticData;
using Modules.Logging;
using Modules.Wallet.Types;
using UnityEngine;

namespace Modules.Analytics.GA
{
    public sealed class GameAnalyticsSystem : AnalyticsSystem, IGameAnalyticsATTListener
    {
        private const AnalyticsSystemCode AnalyticsSystem = AnalyticsSystemCode.GameAnalytics;
        
        public GameAnalyticsSystem(ILogSystem logSystem, IStaticDataService staticDataService) 
            : base(logSystem, staticDataService)
        {
        }

        public override async UniTask InitializeAsync()
        {
            await base.InitializeAsync();
            
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                GameAnalytics.RequestTrackingAuthorization(this);
            }
            else
            {
                InitializeSDK();
            }
            
            LogSystem.SetPrefix("[GameAnalytics] ");
        }

        public void EnableAppLovinIntegration()
        {
            GameAnalyticsILRD.SubscribeMaxImpressions();
        }

        public override void SendCustomEvent(string eventName)
        {
            if (Application.isEditor)
            {
                LogEvent(eventName);

                return;
            }
            
            GameAnalytics.NewDesignEvent(eventName);
            LogEvent(eventName);
        }

        public override void SendCustomEvent(string eventName, Dictionary<string, object> data)
        {
            if (Application.isEditor)
            {
                LogEvent(eventName, data);

                return;
            }
            
            GameAnalytics.NewDesignEvent(eventName, data);
            LogEvent(eventName, data);
        }

        public override void SendCustomEvent(string eventName, float value)
        {
            if (Application.isEditor)
            {
                LogEvent(eventName, value);

                return;
            }
            
            GameAnalytics.NewDesignEvent(eventName, value);
            LogEvent(eventName, value);
        }

        public override void SendInterstitialEvent(AdvertisementAction advertisementAction,
            AdvertisementPlacement placement, AdvertisementsSystemType systemType)
        {
            if (Application.isEditor)
            {
                LogEvent(advertisementAction, placement, systemType);

                return;
            }
            
            SendAdvertisementEvent(advertisementAction, placement, systemType, GAAdType.Interstitial);
        }
        
        public override void SendRewardEvent(AdvertisementAction advertisementAction, AdvertisementPlacement placement, 
            AdvertisementsSystemType systemType)
        {
            if (Application.isEditor)
            {
                LogEvent(advertisementAction, placement, systemType);

                return;
            }
            
            SendAdvertisementEvent(advertisementAction, placement, systemType, GAAdType.RewardedVideo);
        }

        public override void SendErrorEvent(LogLevel logLevel, string message)
        {
            if (Application.isEditor)
            {
                LogEvent(logLevel, message);

                return;
            }
            
            GameAnalytics.NewErrorEvent(logLevel.ToGameAnalytics(), message);
            LogEvent(logLevel, message);
        }

        public override void SendProgressEvent(ProgressStatus progressStatus, string levelName, int progressPercent) =>
            SendProgressEvent(progressStatus, string.Empty, levelName, progressPercent);
        
        public override void SendProgressEvent(ProgressStatus progressStatus, string levelType, string levelName, 
            int progressPercent)
        {
            if (Application.isEditor)
            {
                LogEvent(progressStatus, levelType, levelName, progressPercent);

                return;
            }
            
            GAProgressionStatus gameAnalyticsProgressStatus = progressStatus.ToGameAnalytics();
            
            if (string.IsNullOrEmpty(levelType))
                GameAnalytics.NewProgressionEvent(gameAnalyticsProgressStatus, levelName, progressPercent);
            else
                GameAnalytics.NewProgressionEvent(gameAnalyticsProgressStatus, levelType, 
                    levelName, progressPercent);
            
            LogEvent(progressStatus, levelType, levelName, progressPercent);
        }
        
        protected override void SendCustomEventInternal(AnalyticsEventCode eventCode)
        {
            if (IsExistEvent(eventCode, AnalyticsSystem, out string eventName) == false)
                return;

            SendCustomEvent(eventName);
        }

        protected override void SendCustomEventInternal(AnalyticsEventCode eventCode, Dictionary<string, object> data)
        {
            if (IsExistEvent(eventCode, AnalyticsSystem, out string eventName) == false)
                return;

            SendCustomEvent(eventName, data);
        }

        protected override void SendCustomEventInternal(AnalyticsEventCode eventCode, float value)
        {
            if (IsExistEvent(eventCode, AnalyticsSystem, out string eventName) == false)
                return;

            SendCustomEvent(eventName, value);
        }

        private void SendResourceEvent(ResourceFlowType flowType, CurrencyType currencyType, float amount, 
            string itemType, string itemId)
        {
            if (Application.isEditor)
            {
                LogEvent(flowType, currencyType, amount, itemType, itemId);

                return;
            }

            GameAnalytics.NewResourceEvent(flowType.ToGameAnalytics(), 
                ConvertToSnakeCase(currencyType.ToString()), amount, itemType, 
                itemId);
            
            LogEvent(flowType, currencyType, amount, itemType, itemId);
        }

        private void SendAdvertisementEvent(AdvertisementAction advertisementAction, AdvertisementPlacement placement,
            AdvertisementsSystemType advertisementsSystemType, GAAdType advertisementType)
        {
            if (Application.isEditor)
            {
                LogEvent(advertisementAction, placement, advertisementsSystemType);

                return;
            }

            GameAnalytics.NewAdEvent(advertisementAction.ToGameAnalytics(), advertisementType, 
                ConvertToSnakeCase(advertisementsSystemType.ToString()), 
                ConvertToSnakeCase(placement.ToString()));
            
            LogEvent(advertisementAction, placement, advertisementsSystemType);
        }

        public void GameAnalyticsATTListenerNotDetermined()
        {
            InitializeSDK();
        }

        public void GameAnalyticsATTListenerRestricted()
        {
            InitializeSDK();
        }

        public void GameAnalyticsATTListenerDenied()
        {
            InitializeSDK();
        }

        public void GameAnalyticsATTListenerAuthorized()
        {
            InitializeSDK();
        }

        private void InitializeSDK()
        {
            GameAnalytics.Initialize();
            GameAnalytics.SetBuildAllPlatforms(Application.version);
        } 
    }
}