using System;
using Cysharp.Threading.Tasks;
using Modules.Advertisements.Types;
using Modules.Probabilities;
using Modules.TimeUtilities.Timers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Advertisements.Systems
{
    public abstract class AdvertisementsSystem : IDisposable, IAdvertisementsSystem
    {
        private readonly AudioState _audioState = new();
        private readonly CountdownTimer _canShowTimer = new();
        private float _minAdvertisementShowDelay;
        private bool _isEnabledInterstitial = true;
        private bool _isInitialized;

        public virtual void Dispose()
        {
            _canShowTimer.Dispose();
        }

        public abstract event Action<AdvertisementRevenue> RevenueReceived;

        public abstract bool IsShowInterstitialOrReward { get; }

        public virtual bool CanShowReward => _isInitialized && _canShowTimer.IsRunning == false;

        public virtual bool CanShowInterstitial => _isEnabledInterstitial && _isInitialized 
                                                                          && _canShowTimer.IsRunning == false;

        public abstract AdvertisementsSystemType SystemType { get; }

        public abstract UniTask InitializeAsync();

        public void DisableInterstitial() =>
            _isEnabledInterstitial = false;

        public void EnableInterstitial() =>
            _isEnabledInterstitial = true;

        public abstract bool TryShowBanner();

        public abstract void HideBanner();
        
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
            
            _canShowTimer.Start(_minAdvertisementShowDelay);
            
            StartInterstitialBehaviour(onCloseCallback: onCloseCallback, onShowCallback: onShowCallback, 
                onClickCallback: onClickCallback);

            return true;
        }

        [Button, HideInEditorMode]
        public bool TryShowReward(Action onSuccessCallback = null, Action onCloseCallback = null,
            Action onShowCallback = null, Action onClickCallback = null)
        {
            if (CanShowReward == false)
                return false;
            
            _canShowTimer.Start(_minAdvertisementShowDelay);
            
            StartRewardBehaviour(onSuccessCallback: onSuccessCallback, onCloseCallback: onCloseCallback, 
                onShowCallback: onShowCallback, onClickCallback: onClickCallback);

            return true;
        }
        
        protected void SetMinAdvertisementShowDelay(float minAdvertisementShowDelay) =>
            _minAdvertisementShowDelay = minAdvertisementShowDelay;

        protected void SetInitializeFlag() => _isInitialized = true;
        
        protected abstract void StartInterstitialBehaviour(Action onCloseCallback = null, Action onShowCallback = null, 
            Action onClickCallback = null);

        protected abstract void StartRewardBehaviour(Action onSuccessCallback = null, Action onCloseCallback = null,
            Action onShowCallback = null, Action onClickCallback = null);

        protected void EnableSound() => _audioState.On();

        protected void DisableSound() => _audioState.Off();

        private sealed class AudioState
        {
            private float _originalAudioVolume;
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
