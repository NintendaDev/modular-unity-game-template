using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Analytics.Types;
using Modules.AssetsManagement.Utils;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Modules.Analytics.Configurations
{
    [CreateAssetMenu(fileName = "new CustomAnalyticsEventsHub", menuName = "Modules/Analytics/CustomAnalyticsEventsHub")]
    public sealed class CustomAnalyticsEventsHub : ScriptableObject
    {
        [HorizontalGroup("LoadEvents", width: 0.8f)]
        [FolderPath(RequireExistingPath = true)]
        [SerializeField] private string _eventsFolderPath;
        
        [ValidateInput(nameof(IsUniqueEventData))]
        [SerializeField] private List<CustomAnalyticsEvent> _customEvents;

        public bool IsSentOnce(AnalyticsEventCode eventCode)
        {
            CustomAnalyticsEvent customAnalyticsEvent = _customEvents
                .FirstOrDefault(x => x.EventCode == eventCode);
            
            return customAnalyticsEvent?.IsSetOnce ?? false;
        }

        public bool IsExistEventName(AnalyticsEventCode eventCode, AnalyticsSystemCode analyticsSystemCode, 
            out string eventName)
        {
            CustomAnalyticsEvent customAnalyticsEvent = _customEvents
                .FirstOrDefault(x => x.EventCode == eventCode);
            
            eventName = String.Empty;
            
            if (customAnalyticsEvent == null)
                return false;

            eventName = customAnalyticsEvent.GetEventName(analyticsSystemCode);

            return true;
        }

        private bool IsUniqueEventData(List<CustomAnalyticsEvent> customEvents, ref string errorMessage)
        {
            IEnumerable<AnalyticsEventCode> duplicatesEventCodes = customEvents
                .GroupBy(customEvent => customEvent.EventCode)
                .Where(group => group.Count() > 1)
                .Select(g => g.Key);
            
            if (duplicatesEventCodes.Count() > 0)
            {
                errorMessage = $"Duplicate events codes found: {string.Join(",", duplicatesEventCodes)}";

                return false;
            }

            return true;
        }
        
        [HorizontalGroup("LoadEvents")]
        [Button]
        private void ReadFolder() =>
            ReadFolder(_eventsFolderPath, ref _customEvents);

        private void ReadFolder(string configurationsFolder, ref List<CustomAnalyticsEvent> events)
        {
#if UNITY_EDITOR
            events = AssetsFinder.GetAssetsFromFolder<CustomAnalyticsEvent>(configurationsFolder);
            AssetDatabase.SaveAssets();
#endif
        }
    }
}