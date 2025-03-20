using Modules.Localization.Core.Detectors;
using Modules.Localization.Core.Types;
using Modules.Localization.I2System;
using Zenject;

namespace Game.Application.Bootstrap
{
    public class LocalizationSystemInstaller : Installer<LocalizationSystemInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ConstantLanguageDetector>()
                .AsSingle()
                .WithArguments(Language.English);
            
            Container.BindInterfacesTo<I2LocalizationSystem>().AsSingle();
        }
    }
}