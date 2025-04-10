using UnityEngine;

namespace Modules.Analytics.Configurations
{
    [CreateAssetMenu(fileName = "new AnalyticsParamsNames", 
        menuName = "Modules/Analytics/AnalyticsParamsNames")]
    public sealed class AnalyticsParamsNames : ScriptableObject
    {
        [field: SerializeField] public string LevelCode { get; private set; } = "level_code";
        
        [field: SerializeField] public string LevelBootStage { get; private set; } = "levelboot_stage";
        
        [field: SerializeField] public string GameBootStage { get; private set; } = "gameboot_stage";
        
        [field: SerializeField] public string ProgressStatus { get; private set; } = "progress_status";
        
        [field: SerializeField] public string LevelName { get; private set; } = "level_name";

        [field: SerializeField] public string LevelType { get; private set; } = "level_type";
        
        [field: SerializeField] public string ProgressPercent { get; private set; } = "progress_percent";
        
        [field: SerializeField] public string AdvertisementPlacement { get; private set; } = "ad_placement";
        
        [field: SerializeField] public string AdvertisementPlatform { get; private set; } = "ad_platform";

        [field: SerializeField] public string AdvertisementSource { get; private set; } = "ad_source";
        
        [field: SerializeField] public string AdvertisementUnitName { get; private set; } = "ad_unit_name";
        
        [field: SerializeField] public string AdvertisementFormat { get; private set; } = "ad_format";
        
        [field: SerializeField] public string AdvertisementImpression { get; private set; } = "ad_impression";
        
        [field: SerializeField] public string Value { get; private set; } = "value";

        [field: SerializeField] public string Currency { get; private set; } = "currency";
        
        [field: SerializeField] public string Interstitial { get; private set; } = "INTER";
        
        [field: SerializeField] public string Reward { get; private set; } = "REWARDED";
    }
}