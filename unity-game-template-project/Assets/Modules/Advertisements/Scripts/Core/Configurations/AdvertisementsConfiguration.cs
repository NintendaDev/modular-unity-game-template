using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Advertisements.Configurations
{
    [CreateAssetMenu(fileName = "new AdvertisimentsConfiguration", 
        menuName = "Modules/Advertisements/AdvertisementsConfiguration")]
    public sealed class AdvertisementsConfiguration : ScriptableObject
    {
        [LabelWidth(250)]
        [field: SerializeField, Range(0, 1)]
        public float InterstitialOnStartLevelProbability { get; private set; } = 1f;

        [LabelWidth(250)]
        [field: SerializeField, Range(0, 1)] 
        public float InterstitialOnRestartLevelProbability { get; private set; } = 1f;

        [LabelWidth(250)]
        [field: SerializeField, Range(0, 1)] 
        public float InterstitialOnExitLevelProbability { get; private set; } = 1f;

        [LabelWidth(250)]
        [field: SerializeField, MinValue(0)] 
        public int MaxLevelRestartsWithAdvertisiment { get; private set; } = 2;
    }
}
