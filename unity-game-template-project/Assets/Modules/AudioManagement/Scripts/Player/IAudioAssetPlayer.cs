using Cysharp.Threading.Tasks;
using Modules.AudioManagement.Types;
using UnityEngine;

namespace Modules.AudioManagement.Player
{
    public interface IAudioAssetPlayer
    {
        public bool IsInitialized { get; }
        
        public void Initialize();

        public UniTask WarmupAsync(params AudioCode[] audioCodes);

        public bool IsPlaying(AudioCode audioCode);
        
        public bool IsPlaying(AudioCode audioCode, Transform playPosition);

        public bool IsPaused(AudioCode audioCode);
        
        public bool IsPaused(AudioCode audioCode, Transform playPosition);
        
        public UniTask<bool> TryPlayAsync(AudioCode audioCode);
        
        public UniTask<bool> TryPlayAsync(AudioCode audioCode, Transform playPosition);
        
        public bool TryPauseSound(AudioCode audioCode);

        public bool TryPauseSound(AudioCode audioCode, Transform playPosition);
        
        public bool TryStopSound(AudioCode audioCode);

        public bool TryStopSound(AudioCode audioCode, Transform playPosition);
        
        public void StopAll();
        
        public void PauseAll();
        
        public void UnpauseAll();
    }
}