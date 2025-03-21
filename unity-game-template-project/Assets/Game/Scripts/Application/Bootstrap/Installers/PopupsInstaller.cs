using Modules.PopupsSystem;
using Modules.PopupsSystem.UI.Factories;
using Zenject;

namespace Game.Application.Bootstrap
{
    public class PopupsInstaller : Installer<PopupsInstaller>
    {
        public override void InstallBindings()
        {
            BindPopupsService();
        }
        
        private void BindPopupsService()
        {
            Container.BindInterfacesAndSelfTo<InfoPopupFactory>().AsSingle().WhenInjectedInto<PopupFactory>();
            Container.BindInterfacesAndSelfTo<ErrorPopupFactory>().AsSingle().WhenInjectedInto<PopupFactory>();
            Container.Bind<IPopupFactory>().To<PopupFactory>().AsSingle();

            Container.BindInterfacesTo<Popups>().AsSingle();
        }
    }
}