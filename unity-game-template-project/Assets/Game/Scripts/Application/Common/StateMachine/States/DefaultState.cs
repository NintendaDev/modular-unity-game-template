using Cysharp.Threading.Tasks;
using Modules.StateMachines;
using Modules.StateMachines.States;
using System;
using Modules.EventBus;
using Modules.Logging;

namespace Game.Application.Common
{
    public abstract class DefaultState : IState
    {
        private readonly Type _stateType;

        protected DefaultState(IStateMachine stateMachine, ISignalBus signalBus, ILogSystem logSystem)
        {
            StateMachine = stateMachine;
            LogSystem = logSystem;
            StateSignalBus = signalBus;
            _stateType = GetType();
        }

        protected IStateMachine StateMachine { get; }

        protected ISignalBus StateSignalBus { get; }

        protected ILogSystem LogSystem { get; }

        public virtual UniTask Enter()
        {
            LogSystem.Log($"Enter {_stateType} state");

            return default;
        }

        public virtual UniTask Exit()
        {
            LogSystem.Log($"Exit {_stateType} state");

            return default;
        }
    }
}
