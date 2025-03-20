using System;
using Modules.Advertisements.Systems;
using Modules.Advertisements.Types;
using Modules.Analytics;

namespace Modules.Advertisements.AnalyticsAddon
{
    public sealed class AdvertisementsFacade : IDisposable
    {
        private readonly IAdvertisementsSystem _advertisementsSystem;
        private readonly IAnalyticsSystem _analyticsSystem;

        public AdvertisementsFacade(IAdvertisementsSystem advertisementsSystem, IAnalyticsSystem analyticsSystem)
        {
            _advertisementsSystem = advertisementsSystem;
            _analyticsSystem = analyticsSystem;

            _advertisementsSystem.RevenueReceived += OnRevenueReceive;
        }

        public void Dispose()
        {
            _advertisementsSystem.RevenueReceived -= OnRevenueReceive;
        }

        public bool IsShowInterstitialOrReward => _advertisementsSystem.IsShowInterstitialOrReward;

        public bool TryShowBanner() => _advertisementsSystem.TryShowBanner();

        public bool TryShowInterstitial(AdvertisementPlacement placement, Action onCloseCallback = null)
        {
            _analyticsSystem.SendInterstitialEvent(AdvertisementAction.Request, placement, 
                _advertisementsSystem.SystemType);
            
            bool isSuccessShow = _advertisementsSystem.TryShowInterstitial(onCloseCallback: onCloseCallback,
                onClickCallback: () =>
                {
                    _analyticsSystem.SendInterstitialEvent(AdvertisementAction.Clicked, placement,
                        _advertisementsSystem.SystemType);
                },
                
                onShowCallback: () =>
                {
                    _analyticsSystem.SendInterstitialEvent(AdvertisementAction.Show, placement,
                        _advertisementsSystem.SystemType);
                });

            if (isSuccessShow == false)
            {
                _analyticsSystem.SendInterstitialEvent(AdvertisementAction.FailedShow, placement, 
                    _advertisementsSystem.SystemType);
            }
            
            return isSuccessShow;
        }

        public bool TryShowReward(AdvertisementPlacement placement, Action onSuccessCallback = null, 
            Action onCloseCallback = null)
        {
            _analyticsSystem.SendRewardEvent(AdvertisementAction.Request, placement, 
                _advertisementsSystem.SystemType);
            
            bool isSuccessShow = _advertisementsSystem.TryShowReward(onCloseCallback: onCloseCallback,
                onSuccessCallback: () =>
                {
                    _analyticsSystem.SendRewardEvent(AdvertisementAction.RewardReceived, placement, 
                        _advertisementsSystem.SystemType);
                    
                    onSuccessCallback?.Invoke();
                },
                onShowCallback: () =>
                {
                    _analyticsSystem.SendRewardEvent(AdvertisementAction.Show, placement,
                        _advertisementsSystem.SystemType);
                },
                onClickCallback: () =>
                {
                    _analyticsSystem.SendRewardEvent(AdvertisementAction.Clicked, placement,
                        _advertisementsSystem.SystemType);
                }
                );
            
            if (isSuccessShow == false)
            {
                _analyticsSystem.SendRewardEvent(AdvertisementAction.FailedShow, placement, 
                    _advertisementsSystem.SystemType);
            }

            return isSuccessShow;
        }
        
        private void OnRevenueReceive(AdvertisementRevenue revenue)
        {
            if (_analyticsSystem is IAdRevenueAnalytics revenueAnalytics)
                revenueAnalytics.SendAdvertisementRevenueEvent(revenue);
        }
    }
}