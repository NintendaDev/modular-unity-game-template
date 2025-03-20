using System;
using Cysharp.Threading.Tasks;
using Modules.AssetsManagement.AddressablesOperations;
using Modules.AssetsManagement.StaticData;
using Modules.AudioManagement.Types;
using Sirenix.OdinInspector;
using Sonity;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modules.AudioManagement.Player
{
    public sealed class AudioAssetPlayer : IDisposable, IAudioAssetPlayer
    {
        private SoundManager _soundManager;
        private readonly SoundEventsRegistry _soundEventsRegistry;
        private bool _isPausedAll;

        public AudioAssetPlayer(IAddressablesService addressablesService, IStaticDataService staticDataService)
        {
            _soundEventsRegistry = new SoundEventsRegistry(addressablesService, staticDataService);
        }

        public bool IsInitialized { get; private set; }

        public void Dispose()
        {
            if (_soundManager != null)
                StopAll();
            
            _soundEventsRegistry.Dispose();
        }

        public void Initialize()
        {
            if (IsInitialized)
                return;
            
            _soundEventsRegistry.Initialize();
            _soundManager = Object.FindObjectOfType<SoundManager>();

            if (_soundManager == null)
            {
                _soundManager = new GameObject(nameof(SoundManager)).AddComponent<SoundManager>();
                Object.DontDestroyOnLoad(_soundManager);
            }

            IsInitialized = true;
        }

        public async UniTask WarmupAsync(params AudioCode[] audioCodes)
        {
            foreach (AudioCode audioCode in audioCodes)
            {
                if (audioCode == AudioCode.None)
                    continue;
                
                await _soundEventsRegistry.TryLoadAudioAsync(audioCode);
            }
        }

        public bool IsPlaying(AudioCode audioCode) => IsPlaying(audioCode, _soundManager.transform);

        public bool IsPlaying(AudioCode audioCode, Transform playPosition)
        {
            if (audioCode == AudioCode.None)
                return false;
            
            if (TryGetSoundEventState(audioCode, playPosition, out SoundEventState soundEventState) == false)
                return false;
            
            return soundEventState == SoundEventState.Playing;
        }
        
        public bool IsPaused(AudioCode audioCode) => IsPaused(audioCode, _soundManager.transform);
        
        public bool IsPaused(AudioCode audioCode, Transform playPosition)
        {
            if (audioCode == AudioCode.None)
                return true;
            
            if (TryGetSoundEventState(audioCode, playPosition, out SoundEventState soundEventState) == false)
                return false;
            
            return soundEventState == SoundEventState.Paused;
        }
        
        [Button, HideInEditorMode]
        public async UniTask<bool> TryPlayAsync(AudioCode audioCode) => 
            await TryPlayAsync(audioCode, _soundManager.transform);
        
        public async UniTask<bool> TryPlayAsync(AudioCode audioCode, Transform playPosition)
        {
            if (_isPausedAll || audioCode == AudioCode.None)
                return false;
            
            if (_soundEventsRegistry.IsExistSoundEvent(audioCode, out SoundEvent soundEvent) == false)
            {
                if (await _soundEventsRegistry.TryLoadAudioAsync(audioCode) == false)
                    return false;
                
                _soundEventsRegistry.IsExistSoundEvent(audioCode, out soundEvent);
            }
            
            soundEvent.Play(playPosition);
            
            if (soundEvent.GetSoundEventState(playPosition) == SoundEventState.Paused)
                soundEvent.Unpause(playPosition);
            
            bool isSuccess = soundEvent.GetSoundEventState(playPosition) == SoundEventState.Playing;
            
            return isSuccess;
        }
        
        [Button, HideInEditorMode]
        public bool TryPauseSound(AudioCode audioCode) => TryPauseSound(audioCode, _soundManager.transform);
        
        public bool TryPauseSound(AudioCode audioCode, Transform playPosition)
        {
            if (audioCode == AudioCode.None ||
                _soundEventsRegistry.IsExistSoundEvent(audioCode, out SoundEvent soundEvent) == false)
            {
                return false;
            }
            
            soundEvent.Pause(playPosition);

            return true;
        }
        
        [Button, HideInEditorMode]
        public bool TryStopSound(AudioCode audioCode) => TryStopSound(audioCode, _soundManager.transform);
        
        public bool TryStopSound(AudioCode audioCode, Transform playPosition)
        {
            if (audioCode == AudioCode.None ||
                _soundEventsRegistry.IsExistSoundEvent(audioCode, out SoundEvent soundEvent) == false)
            {
                return false;
            }
            
            SoundEventState currentSoundState = soundEvent.GetSoundEventState(playPosition);

            if (currentSoundState == SoundEventState.NotPlaying)
                return true;
            
            soundEvent.Stop(playPosition);

            return true;
        }

        [Button, HideInEditorMode]
        public void StopAll()
        {
            _soundManager.StopEverything(allowFadeOut: false);
        }

        [Button, HideInEditorMode]
        public void PauseAll()
        {
            _isPausedAll = true;
            _soundManager.PauseEverything();
        }

        [Button, HideInEditorMode]
        public void UnpauseAll()
        {
            _isPausedAll = false;
            _soundManager.UnpauseEverything();
        }

        private bool TryGetSoundEventState(AudioCode soundCode, Transform playPosition, out SoundEventState state)
        {
            state = SoundEventState.NotPlaying;
            
            if (_soundEventsRegistry.IsExistSoundEvent(soundCode, out SoundEvent soundEvent) == false)
                return false;
            
            state = soundEvent.GetSoundEventState(playPosition);

            return true;
        }
    }
}