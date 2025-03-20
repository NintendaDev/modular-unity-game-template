using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Analytics.Types;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Analytics.Configurations
{
    [CreateAssetMenu(fileName = "new CustomAnalyticsEvent", menuName = "Modules/Analytics/CustomAnalyticsEvent")]
    public sealed class CustomAnalyticsEvent : ScriptableObject
    {
        [ValidateInput(nameof(IsNotNoneEventCode))]
        [SerializeField] private AnalyticsEventCode _eventCode;
        
        [SerializeField] private bool _isSentOnce;

        [Required, SerializeField] private string _defaultName;

        [ValidateInput(nameof(IsUniqueEventName))] 
        [SerializeField] private List<CustomEventName> _eventNames;

        public AnalyticsEventCode EventCode => _eventCode;
        
        public bool IsSetOnce => _isSentOnce;

        public string GetEventName(AnalyticsSystemCode analyticsSystemCode)
        {
            CustomEventName customEventName = _eventNames
                .FirstOrDefault(x => x.System == analyticsSystemCode);

            if (customEventName == null)
                return _defaultName;

            return customEventName.Name;
        }

        private bool IsUniqueEventName(List<CustomEventName> eventNames, ref string errorMessage)
        {
            if (eventNames.GroupBy(x => x.System).Count() != eventNames.Count)
            {
                errorMessage = "Duplicate analytics system codes found";

                return false;
            }

            return true;
        }

        private bool IsNotNoneEventCode(AnalyticsEventCode analyticsEventCode, ref string errorMessage)
        {
            if (analyticsEventCode == AnalyticsEventCode.None)
            {
                errorMessage = "The event code cannot be None";

                return false;
            }

            return true;
        }
        
        [Serializable]
        public class CustomEventName
        {
            [HorizontalGroup("Event")]
            [ValidateInput(nameof(IsNotNoneAnalyticsSystemCode))] 
            [SerializeField] private AnalyticsSystemCode _system;
        
            [HorizontalGroup("Event")]
            [Required] 
            [SerializeField] private string _name;
        
            public AnalyticsSystemCode System => _system;
        
            public string Name => _name;
        
            private bool IsNotNoneAnalyticsSystemCode(AnalyticsSystemCode systemCode, ref string errorMessage)
            {
                if (systemCode == AnalyticsSystemCode.None)
                {
                    errorMessage = "The analytics system code cannot be None";

                    return false;
                }

                return true;
            }
        }
    }
}