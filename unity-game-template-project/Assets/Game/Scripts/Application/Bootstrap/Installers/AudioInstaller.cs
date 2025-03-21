using Modules.AudioManagement.Mixer;
using Zenject;

namespace Game.Application.Bootstrap
{
    public class AudioInstaller : Installer<AudioInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AudioMixerSystem>().AsSingle();
        }
    }
}