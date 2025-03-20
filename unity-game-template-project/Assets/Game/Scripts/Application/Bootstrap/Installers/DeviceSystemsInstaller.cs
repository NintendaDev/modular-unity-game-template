using Modules.ControllManagement.Detectors;
using Modules.Device.Detecting;
using Modules.Device.Performance;
using Zenject;

namespace Game.Application.Bootstrap
{
    public class DeviceSystemsInstaller : Installer<DeviceSystemsInstaller>
    {
        public override void InstallBindings()
        {
            BindDeviceDetector();
            BindTouchDetector();
            BindDevicePerformanceConfigurationGetter();
            BindSystemPerformanceSetter();
        }

        private void BindDeviceDetector() =>
            Container.BindInterfacesTo<UnityDeviceDetector>().AsSingle();

        private void BindTouchDetector() =>
            Container.BindInterfacesTo<LegacyTouchDetector>().AsSingle();

        private void BindDevicePerformanceConfigurationGetter() =>
            Container.BindInterfacesTo<PerformaceConfigurationProvider>().AsSingle();

        private void BindSystemPerformanceSetter() =>
            Container.Bind<SystemPerformanceSetter>().AsSingle();
    }
}