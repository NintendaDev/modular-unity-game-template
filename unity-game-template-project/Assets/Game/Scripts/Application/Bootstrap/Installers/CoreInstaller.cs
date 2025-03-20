using Modules.EventBus;
using Modules.Logging;
using Zenject;

namespace Game.Application.Bootstrap
{
    public class CoreInstaller : Installer<CoreInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<LogSystem>().AsSingle();
            Container.BindInterfacesTo<SignalBus>().AsSingle();
        }
    }
}