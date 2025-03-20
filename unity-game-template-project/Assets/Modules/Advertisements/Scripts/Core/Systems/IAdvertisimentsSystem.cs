using System;
using Cysharp.Threading.Tasks;
using Modules.Advertisements.Types;

namespace Modules.Advertisements.Systems
{
    public interface IAdvertisementsSystem
    {
        public event Action<AdvertisementRevenue> RevenueReceived;
        
        public AdvertisementsSystemType SystemType { get; }
        
        public bool IsShowInterstitialOrReward { get; }

        public bool CanShowInterstitial { get; }

        public bool CanShowReward { get; }
        
        public UniTask InitializeAsync();

        public void DisableInterstitial();

        public bool TryShowBanner();
        
        public void HideBanner();

        public bool TryShowInterstitial(float probability, Action onCloseCallback = null, Action onShowCallback = null, 
            Action onClickCallback = null);

        public bool TryShowInterstitial(Action onCloseCallback = null, Action onShowCallback = null, 
            Action onClickCallback = null);

        public bool TryShowReward(Action onSuccessCallback = null, Action onCloseCallback = null, 
            Action onShowCallback = null, Action onClickCallback = null);
    }
}