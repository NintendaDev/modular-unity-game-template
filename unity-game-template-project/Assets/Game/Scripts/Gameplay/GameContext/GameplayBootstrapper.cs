using Cysharp.Threading.Tasks;
using Game.Application.Common;
using Modules.StateMachines;
using Zenject;

namespace Game.Gameplay.GameContext
{
    public sealed class GameplayBootstrapper : IInitializable
    {
        private readonly SceneStateMachine _sceneStateMachine;
        private readonly StatesFactory _statesFactory;

        public GameplayBootstrapper(SceneStateMachine sceneStateMachine, StatesFactory statesFactory)
        {
            _sceneStateMachine = sceneStateMachine;
            _statesFactory = statesFactory;
        }

        public void Initialize()
        {
            _sceneStateMachine.RegisterState(_statesFactory.Create<BootstrapSceneState>());
            _sceneStateMachine.RegisterState(_statesFactory.Create<StartSceneState>());
            _sceneStateMachine.RegisterState(_statesFactory.Create<PlaySceneState>());
            _sceneStateMachine.RegisterState(_statesFactory.Create<PauseSceneState>());
            _sceneStateMachine.RegisterState(_statesFactory.Create<GameOverSceneState>());
            _sceneStateMachine.RegisterState(_statesFactory.Create<FinishSceneState>());

            _sceneStateMachine.SwitchState<BootstrapSceneState>().Forget();
        }
    }
}
