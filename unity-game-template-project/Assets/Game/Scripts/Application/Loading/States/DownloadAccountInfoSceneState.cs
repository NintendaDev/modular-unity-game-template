using Cysharp.Threading.Tasks;
using Game.Application.Common;
using Modules.EventBus;
using Modules.Logging;
using Modules.NetworkAccount;

namespace Game.Application.Loading
{
    public sealed class DownloadAccountInfoSceneState : SceneState
    {
        private readonly INetworkAccount _networkAccount;

        public DownloadAccountInfoSceneState(SceneStateMachine stateMachine, ISignalBus signalBus,
            ILogSystem logSystem, INetworkAccount networkAccount) 
            : base(stateMachine, signalBus, logSystem)
        {
            _networkAccount = networkAccount;
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            await _networkAccount.InitializeAsync();
            await StateMachine.SwitchState<LoadPlayerProgressSceneState>();
        }
    }
}
