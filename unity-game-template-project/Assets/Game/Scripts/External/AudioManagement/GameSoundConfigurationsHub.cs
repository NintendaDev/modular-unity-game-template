using Modules.AudioManagement.Configurations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.External.AudioManagement
{
    [CreateAssetMenu(fileName = "new GameSoundConfigurationsHub", 
        menuName = "GameTemplate/AudioManagement/GameSoundConfigurationsHub")]
    public sealed class GameSoundConfigurationsHub : ScriptableObject
    {
        [field: SerializeField, Required] public SoundConfiguration GameHub { get; private set; }
        
        [field: SerializeField, Required] public SoundConfiguration Gameplay { get; private set; }
        
        [field: SerializeField, Required] public SoundConfiguration UI { get; private set; }
    }
}