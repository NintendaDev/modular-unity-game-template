using Cysharp.Threading.Tasks;
using Game.Application.Common;
using Game.Application.GameHub;
using Game.Application.Loading;
using Game.Gameplay.States;
using Modules.StateMachines;
using UnityEngine;
using Zenject;

namespace Game.Application.Bootstrap
{
    public sealed class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private StatesFactory _statesFactory;

        [Inject]
        void Construct(GameStateMachine gameStateMachine, StatesFactory statesFactory)
        {
            _gameStateMachine = gameStateMachine;
            _statesFactory = statesFactory;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);

            _gameStateMachine.RegisterState(_statesFactory.Create<BootstrapGameState>());
            _gameStateMachine.RegisterState(_statesFactory.Create<GameLoadingState>());
            _gameStateMachine.RegisterState(_statesFactory.Create<GameHubGameState>());
            _gameStateMachine.RegisterState(_statesFactory.Create<GameplayGameState>());

            _gameStateMachine.SwitchState<BootstrapGameState>().Forget();
        }
    }
}
