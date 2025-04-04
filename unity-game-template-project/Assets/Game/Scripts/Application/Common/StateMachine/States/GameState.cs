using Modules.EventBus;
using Modules.Logging;

namespace Game.Application.Common
{
    public abstract class GameState : DefaultState
    {
        protected GameState(GameStateMachine stateMachine, ISignalBus signalBus, ILogSystem logSystem) 
            : base(stateMachine, signalBus, logSystem)
        {
        }
    }
}