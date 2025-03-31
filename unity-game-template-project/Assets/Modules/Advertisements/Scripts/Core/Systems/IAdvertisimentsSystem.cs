using System;
using Cysharp.Threading.Tasks;
using Modules.Advertisements.Types;

namespace Modules.Advertisements.Systems
{
    public interface IAdvertisementsSystem
    {
        public event Action<AdvertisementRevenue> RevenueReceived;
        
        public Types.AdvertisementsSystemType Type { get; }
        
        public bool IsShowInterstitialOrReward { get; }
        
        public bool CanShowBanner { get; }

        public bool CanShowInterstitial { get; }

        public bool CanShowReward { get; }
        
        public UniTask InitializeAsync();

        public void DisableInterstitial();
        
        public void DisableBanner();

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