using Cysharp.Threading.Tasks;
using Game.GameLifeCycle.GameHub.States;
using Game.Infrastructure.StateMachineComponents;
using Game.Infrastructure.StateMachineComponents.States;
using Modules.Analytics;
using Modules.Analytics.Types;
using Modules.EventBus;
using Modules.Logging;

namespace Game.GameLifeCycle.Loading
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
