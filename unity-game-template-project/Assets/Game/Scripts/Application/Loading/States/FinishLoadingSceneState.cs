using Cysharp.Threading.Tasks;
using Game.Application.Common;
using Game.Application.GameHub;
using Modules.Analytics;
using Modules.Analytics.Types;
using Modules.EventBus;
using Modules.Logging;

namespace Game.Application.Loading
{
    public sealed class FinishLoadingSceneState : AnalyticsGameState
    {
        public FinishLoadingSceneState(GameStateMachine stateMachine, ILogSystem logSystem, ISignalBus signalBus,
            IAnalyticsSystem analyticsSystem) 
            : base(stateMachine, signalBus, logSystem, analyticsSystem)
        {
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            await StateMachine.SwitchState<GameHubGameState>();
            SendAnalyticsEvent(AnalyticsEventCode.GameBootMainMenu);
        }
    }
}
