using Game.Application.Common;
using Modules.StateMachines;
using Zenject;

namespace Game.Application.Loading
{
    public sealed class GameLoadingSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameLoadingSceneBootstrapper>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StatesFactory>().AsSingle();
            Container.Bind<SceneStateMachine>().AsSingle();
        }
    }
}
