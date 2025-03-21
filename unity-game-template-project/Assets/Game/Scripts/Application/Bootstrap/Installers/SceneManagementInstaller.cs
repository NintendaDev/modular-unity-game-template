using Game.Application.GameHub;
using Game.Application.LevelLoading;
using Modules.SceneManagement;
using Zenject;

namespace Game.Application.Bootstrap
{
    public class SceneManagementInstaller : Installer<SceneManagementInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SingleSingleSceneLoader>().AsSingle();
            Container.BindInterfacesTo<LevelLoaderService>().AsSingle();
        }
    }
}