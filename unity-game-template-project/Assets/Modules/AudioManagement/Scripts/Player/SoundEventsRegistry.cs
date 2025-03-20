using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Modules.AssetsManagement.AddressablesOperations;
using Modules.AssetsManagement.StaticData;
using Modules.AudioManagement.Configurations;
using Modules.AudioManagement.Types;
using Sonity;

namespace Modules.AudioManagement.Player
{
    public sealed class SoundEventsRegistry : IDisposable
    {
        private readonly IAddressablesService _addressablesService;
        private readonly IStaticDataService _staticDataService;
        private readonly Dictionary<AudioCode, SoundEvent> _loadedSoundEvents = new();
        private readonly Dictionary<AudioCode, AssetReferenceSoundEvent> _loadedSoundReferences = new();
        private readonly HashSet<AudioCode> _lockedLoadAudioCodes = new();
        private SoundConfiguration _soundConfiguration;

        public SoundEventsRegistry(IAddressablesService addressablesService, IStaticDataService staticDataService)
        {
            _addressablesService = addressablesService;
            _staticDataService = staticDataService;
        }

        public void Dispose()
        {
            ReleaseSounds();
        }

        public void Initialize()
        {
            _soundConfiguration = _staticDataService.GetConfiguration<SoundConfiguration>();
        }
        
        public bool IsExistSoundEvent(AudioCode soundCode, out SoundEvent soundEvent) =>
            _loadedSoundEvents.TryGetValue(soundCode, out soundEvent);

        public async UniTask<bool> TryLoadAudioAsync(AudioCode audioCode)
        {
            if (_loadedSoundReferences.ContainsKey(audioCode))
                return true;
            
            if (_lockedLoadAudioCodes.Contains(audioCode))
                return false;

            if (_soundConfiguration.IsExistAudioAsset(audioCode, out AudioAsset audioAsset) == false)
                return false;
            
            _lockedLoadAudioCodes.Add(audioCode);
            
            SoundEvent soundEvent = await _addressablesService
                .LoadAsync<SoundEvent>(audioAsset.Reference);
                
            _loadedSoundEvents.Add(audioAsset.Code, soundEvent);
            
            if (_loadedSoundReferences.ContainsKey(audioCode))
                _loadedSoundReferences[audioAsset.Code] = audioAsset.Reference;
            else
                _loadedSoundReferences.Add(audioAsset.Code, audioAsset.Reference);

            _lockedLoadAudioCodes.Remove(audioCode);

            return true;
        }

        public void ReleaseSounds()
        {
            foreach (AssetReferenceSoundEvent soundReference in _loadedSoundReferences.Values)
                _addressablesService.Release(soundReference);
            
            _loadedSoundReferences.Clear();
            _loadedSoundEvents.Clear();
        }
    }
}