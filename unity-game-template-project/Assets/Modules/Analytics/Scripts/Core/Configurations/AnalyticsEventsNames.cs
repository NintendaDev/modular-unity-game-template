using UnityEngine;

namespace Modules.Analytics.Configurations
{
    [CreateAssetMenu(fileName = "new AnalyticsEventsNames", 
        menuName = "Modules/Analytics/AnalyticsEventsNames")]
    public sealed class AnalyticsEventsNames : ScriptableObject
    {
        [field: SerializeField] public string LevelProgress { get; private set; } = "level_progress";
        
        [field: SerializeField] public string GameBoot { get; private set; } = "gameboot";
        
        [field: SerializeField] public string LevelBoot{ get; private set; } = "levelboot";
    }
}