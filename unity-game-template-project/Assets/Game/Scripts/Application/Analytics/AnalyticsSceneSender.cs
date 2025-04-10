using Modules.Analytics;
using Modules.Analytics.Types;
using UnityEngine;
using Zenject;

namespace Game.Application.Analytics
{
    public sealed class AnalyticsSceneSender : MonoBehaviour
    {
        [SerializeField] private AnalyticsEventCode _analyticsEventCode;
        
        private IAnalyticsSystem _analyticsSystem;

        [Inject]
        private void Construct(IAnalyticsSystem analyticsSystem)
        {
            _analyticsSystem = analyticsSystem;
        }

        public void Send() => _analyticsSystem.SendCustomEvent(_analyticsEventCode);
    }
}