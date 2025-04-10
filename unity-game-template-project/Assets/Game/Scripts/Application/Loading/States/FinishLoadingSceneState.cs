using Cysharp.Threading.Tasks;
using Game.Application.Analytics;
using Game.Application.Common;
using Game.Application.GameHub;
using Modules.EventBus;
using Modules.Logging;

namespace Game.Application.Loading
{
    public sealed class FinishLoadingSceneState : GameState
    {
        private readonly TemplateAnalyticsSystem _analyticsSystem;

        public FinishLoadingSceneState(GameStateMachine stateMachine, ILogSystem logSystem, ISignalBus signalBus,
            TemplateAnalyticsSystem analyticsSystem) 
            : base(stateMachine, signalBus, logSystem)
        {
            _analyticsSystem = analyticsSystem;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            await StateMachine.SwitchState<GameHubGameState>();
            _analyticsSystem.SendGameBootEvent(GameBootStage.MainMenu);
        }
    }
}
