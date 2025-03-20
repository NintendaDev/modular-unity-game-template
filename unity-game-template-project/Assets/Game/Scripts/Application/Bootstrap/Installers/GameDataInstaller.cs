using Modules.Statistics.Systems;
using Modules.Wallets.Systems;
using Zenject;

namespace Game.Application.Bootstrap
{
    public class GameDataInstaller : Installer<GameDataInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerStatisticsService>().AsSingle();
            Container.BindInterfacesTo<Wallet>().AsSingle();
        }
    }
}