using System;
using Cysharp.Threading.Tasks;
using Modules.Advertisements.Types;
using Modules.Probabilities;
using Modules.TimeUtilities.Timers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Advertisements.Systems
{
    public abstract class AdvertisementsSystem : IAdvertisementsSystem, IDisposable
    {
        private readonly AudioState _audioState = new();
        private readonly CountdownTimer _canShowInterstitialTimer = new();
        private float _minInterstitialShowDelay;
        private bool _isEnabledInterstitial = true;
        private bool _isEnableBanner = true;
        private bool _isInitialized;

        public virtual void Dispose()
        {
            _canShowInterstitialTimer.Dispose();
        }

        public abstract event Action<AdvertisementRevenue> RevenueReceived;

        public abstract bool IsShowInterstitialOrReward { get; }
        
        public virtual bool CanShowBanner => _isInitialized && _isEnableBanner;

        public virtual bool CanShowReward => _isInitialized;

        public virtual bool CanShowInterstitial => _isEnabledInterstitial && _isInitialized 
                                                                          && _canShowInterstitialTimer.IsRunning == false;

        public abstract AdvertisementsSystemType Type { get; }

        public abstract UniTask InitializeAsync();

        public void DisableInterstitial() => _isEnabledInterstitial = false;
        
        public void DisableBanner()
        {
            _isEnableBanner = false;
            DestroyBanner();
        }

        public abstract bool TryShowBanner();

        public abstract void HideBanner();
        
        protected abstract void DestroyBanner();
        
        public bool TryShowInterstitial(float probability, Action onCloseCallback, Action onShowCallback = null, 
            Action onClickCallback = null)
        {
            if (probability.HasChance())
                return TryShowInterstitial(onCloseCallback: onCloseCallback, onShowCallback: onShowCallback, 
                    onClickCallback: onClickCallback);

            return false;
        }

        [Button, HideInEditorMode]
        public bool TryShowInterstitial(Action onCloseCallback, Action onShowCallback = null, 
            Action onClickCallback = null)
        {
            if (CanShowInterstitial == false)
                return false;
            
            StartInterstitialBehaviour(onCloseCallback: CreateCallbackWithInterstitialTimer(onCloseCallback), 
                onShowCallback: onShowCallback, onClickCallback: onClickCallback);

            return true;
        }

        [Button, HideInEditorMode]
        public bool TryShowReward(Action onSuccessCallback = null, Action onCloseCallback = null,
            Action onShowCallback = null, Action onClickCallback = null)
        {
            if (CanShowReward == false)
                return false;
            
            StartRewardBehaviour(onSuccessCallback: onSuccessCallback, 
                onCloseCallback: CreateCallbackWithInterstitialTimer(onCloseCallback), 
                onShowCallback: onShowCallback, onClickCallback: onClickCallback);

            return true;
        }
        
        protected void SetMinAdvertisementShowDelay(float minAdvertisementShowDelay) =>
            _minInterstitialShowDelay = minAdvertisementShowDelay;

        protected void SetInitializeFlag() => _isInitialized = true;
        
        protected abstract void StartInterstitialBehaviour(Action onCloseCallback = null, Action onShowCallback = null, 
            Action onClickCallback = null);

        protected abstract void StartRewardBehaviour(Action onSuccessCallback = null, Action onCloseCallback = null,
            Action onShowCallback = null, Action onClickCallback = null);

        protected void EnableSound() => _audioState.On();

        protected void DisableSound() => _audioState.Off();

        private Action CreateCallbackWithInterstitialTimer(Action sourceCallback)
        {
            return () =>
            {
                _canShowInterstitialTimer.Start(_minInterstitialShowDelay);
                sourceCallback?.Invoke();
            };
        }

        private sealed class AudioState
        {
            private float _originalAudioVolume;
            private float _originalTimeScale;
            private bool _isOffState;

            public void On()
            {
                if (_isOffState == false)
                    return;
                
                AudioListener.volume = _originalAudioVolume;
                
                _isOffState = false;
            }

            public void Off()
            {
                if (_isOffState)
                    return;
                
                Save();
                
                AudioListener.volume = 0;

                _isOffState = true;
            }

            private void Save()
            {
                _originalAudioVolume = AudioListener.volume;
            }
        }
    }
}
