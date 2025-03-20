using System;
using Cysharp.Threading.Tasks;
using Modules.Advertisements.Systems;
using Modules.Advertisements.Types;
using UnityEngine;

namespace Modules.Advertisements.Dummy
{
    public sealed class DummyAdvertisementsSystem : AdvertisementsSystem
    {
        public override event Action<AdvertisementRevenue> RevenueReceived;

        public override AdvertisementsSystemType SystemType => AdvertisementsSystemType.Dummy;
        
        public override bool IsShowInterstitialOrReward => false;

        public override bool CanShowReward => true;

        public override UniTask InitializeAsync()
        {
            Debug.Log("Advertisements initialized");
            
            return UniTask.CompletedTask;
        }

        public override bool TryShowBanner()
        {
            Debug.Log("Banner shown");

            return true;
        }

        public override void HideBanner()
        {
            Debug.Log("Banner hide");
        }

        protected override void StartInterstitialBehaviour(Action onCloseCallback = null, Action onShowCallback = null,
            Action onClickCallback = null)
        {
            Debug.Log("Interstitial Ad show started");
            onShowCallback?.Invoke();
            onClickCallback?.Invoke();
            onCloseCallback?.Invoke();
        }

        protected override void StartRewardBehaviour(Action onSuccessCallback = null, Action onCloseCallback = null,
            Action onShowCallback = null, Action onClickCallback = null)
        {
            Debug.Log("Redard Ad show started");
            onShowCallback?.Invoke();
            onClickCallback?.Invoke();
            onSuccessCallback?.Invoke();
            onCloseCallback?.Invoke();
        }
    }
}
