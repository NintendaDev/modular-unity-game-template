using Cysharp.Threading.Tasks;
using Modules.AudioManagement.Player;
using Modules.AudioManagement.Types;
using Modules.SimplePlatformer.VFX;
using UnityEngine;
using Zenject;

namespace GameTemplate.Gameplay.Common
{
    public class AudioEffect : VisualEffect
    {
        [SerializeField] private AudioCode _audioCode;
        
        private IAudioAssetPlayer _audioAssetPlayer;

        [Inject]
        private void Construct(IAudioAssetPlayer audioAssetPlayer)
        {
            _audioAssetPlayer = audioAssetPlayer;
        }
        
        public override void Play() => _audioAssetPlayer.TryPlayAsync(_audioCode, transform).Forget();
    }
}