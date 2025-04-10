using Cysharp.Threading.Tasks;
using Game.Application.Analytics;
using Game.Application.Common;
using Modules.EventBus;
using Modules.Logging;
using Modules.NetworkAccount;

namespace Game.Application.Loading
{
    public sealed class DownloadAccountInfoSceneState : SceneState
    {
        private readonly INetworkAccount _networkAccount;
        private readonly TemplateAnalyticsSystem _analyticsSystem;

        public DownloadAccountInfoSceneState(SceneStateMachine stateMachine, ISignalBus signalBus,
            ILogSystem logSystem, INetworkAccount networkAccount, TemplateAnalyticsSystem analyticsSystem) 
            : base(stateMachine, signalBus, logSystem)
        {
            _networkAccount = networkAccount;
            _analyticsSystem = analyticsSystem;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            
            _analyticsSystem.SendGameBootEvent(GameBootStage.DownloadAccountInfo);

            await _networkAccount.InitializeAsync();
            await StateMachine.SwitchState<LoadPlayerProgressSceneState>();
        }
    }
}
