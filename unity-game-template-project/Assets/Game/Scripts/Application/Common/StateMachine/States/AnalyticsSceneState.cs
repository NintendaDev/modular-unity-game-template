using Modules.Analytics;
using Modules.Analytics.Types;
using Modules.EventBus;
using Modules.Logging;

namespace Game.Application.Common
{
    public abstract class AnalyticsSceneState : SceneState
    {
        private readonly IAnalyticsSystem _analyticsSystem;

        public AnalyticsSceneState(SceneStateMachine stateMachine, ISignalBus signalBus, ILogSystem logSystem,
            IAnalyticsSystem analyticsSystem)
            : base(stateMachine, signalBus, logSystem)
        {
            _analyticsSystem = analyticsSystem;
        }

        protected void SendAnalyticsEvent(AnalyticsEventCode eventCode) =>
            _analyticsSystem.SendCustomEvent(eventCode);
    }
}
