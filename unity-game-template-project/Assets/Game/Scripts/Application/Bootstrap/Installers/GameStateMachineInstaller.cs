using Game.Application.Common;
using Modules.StateMachines;
using Zenject;

namespace Game.Application.Bootstrap
{
    public class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<StatesFactory>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}