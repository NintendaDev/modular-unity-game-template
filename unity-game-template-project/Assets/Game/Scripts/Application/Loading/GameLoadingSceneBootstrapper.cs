using Cysharp.Threading.Tasks;
using Game.Application.Common;
using Modules.StateMachines;
using Modules.Logging;
using Zenject;

namespace Game.Application.Loading
{
    public sealed class GameLoadingSceneBootstrapper : IInitializable
    {
        private readonly SceneStateMachine _sceneStateMachine;
        private readonly StatesFactory _statesFactory;
        private readonly ILogSystem _logSystem;

        private GameLoadingSceneBootstrapper(SceneStateMachine sceneStateMachine, StatesFactory statesFactory, 
            ILogSystem logSystem)
        {
            _sceneStateMachine = sceneStateMachine;
            _statesFactory = statesFactory;
            _logSystem = logSystem;
        }

        public void Initialize()
        {
            _logSystem.Log("Start loading scene bootstraping");

            _sceneStateMachine.RegisterState(_statesFactory.Create<DownloadAccountInfoSceneState>());
            _sceneStateMachine.RegisterState(_statesFactory.Create<LoadPlayerProgressSceneState>());
            _sceneStateMachine.RegisterState(_statesFactory.Create<FinishLoadingSceneState>());

            _logSystem.Log("Finish loading scene bootstraping");

            _sceneStateMachine.SwitchState<DownloadAccountInfoSceneState>().Forget();
        }
    }
}
