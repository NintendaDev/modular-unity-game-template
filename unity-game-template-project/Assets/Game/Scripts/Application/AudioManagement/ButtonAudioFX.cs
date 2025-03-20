using Cysharp.Threading.Tasks;
using Modules.AudioManagement.Player;
using Modules.AudioManagement.Types;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Game.Application.AudioManagement
{
    public class ButtonAudioFX : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField, Required] private Button _button;
        [SerializeField] private AudioCode _clickSound;
        [SerializeField] private AudioCode _highlightSound;
        
        private IAudioAssetPlayer _audioAssetPlayer;

        [Inject]
        private void Construct(IAudioAssetPlayer audioAssetPlayer)
        {
            _audioAssetPlayer = audioAssetPlayer;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick() => PlaySound(_clickSound);

        public void OnPointerEnter(PointerEventData eventData) => PlaySound(_highlightSound);

        private void PlaySound(AudioCode audioCode)
        {
            if (audioCode == AudioCode.None)
                return;
            
            _audioAssetPlayer.TryPlayAsync(audioCode).Forget();
        }
    }
}