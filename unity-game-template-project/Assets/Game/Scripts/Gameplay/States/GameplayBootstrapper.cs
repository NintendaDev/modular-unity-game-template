using Cysharp.Threading.Tasks;
using Game.Application.Common;
using Modules.StateMachines;
using Zenject;

namespace Game.Gameplay.States
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
            _sceneStateMachine.RegisterState(_statesFactory.Create<StartGameplaySceneState>());
            _sceneStateMachine.RegisterState(_statesFactory.Create<PlayGameplaySceneState>());
            _sceneStateMachine.RegisterState(_statesFactory.Create<PauseGameplaySceneState>());
            _sceneStateMachine.RegisterState(_statesFactory.Create<FinishGameplaySceneState>());

            _sceneStateMachine.SwitchState<BootstrapSceneState>().Forget();
        }
    }
}
