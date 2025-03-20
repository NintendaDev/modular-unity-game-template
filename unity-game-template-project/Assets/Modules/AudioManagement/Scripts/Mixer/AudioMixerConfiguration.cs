using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.AudioManagement.Mixer
{
    [CreateAssetMenu(fileName = "new AudioMixerConfiguration", menuName = "Modules/Audio/AudioMixerConfiguration")]
    public sealed class AudioMixerConfiguration : ScriptableObject
    {
        [field: SerializeField, Required] public UnityEngine.Audio.AudioMixer AudioMixer { get; private set; }

        [field: SerializeField, Required] public string MasterMixerParameter { get; private set; } = "Master";

        [field: SerializeField, Required] public string MusicMixerParameter { get; private set; } = "Music";

        [field: SerializeField, Required] public string EffectsMixerParameter { get; private set; } = "Effects";

        [field: SerializeField, Range(0, 1)] public float DefaultMusicVolumePercent { get; private set; } = 0.85f;

        [field: SerializeField, Range(0, 1)] public float DefaultEffectsVolumePercent { get; private set; } = 1f;
    }
}