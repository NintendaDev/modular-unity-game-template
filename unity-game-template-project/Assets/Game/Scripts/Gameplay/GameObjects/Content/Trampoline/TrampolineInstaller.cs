using Modules.SimplePlatformer.Triggers;
using Modules.SimplePlatformer.VFX;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameTemplate.Gameplay.GameObjects
{
    public class TrampolineInstaller : MonoInstaller
    {
        [SerializeField, Required] private TriggerEventer _triggerEventer;
        [SerializeField, Required] private VisualEffect _launchEffect;
        [SerializeField] private Trampoline.Settings _settings;
        
        public override void InstallBindings()
        {
            Container.Bind<TriggerEventer>()
                .FromInstance(_triggerEventer)
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<VisualEffect>()
                .FromInstance(_launchEffect)
                .AsSingle();
            
            Container.BindInterfacesTo<Trampoline>()
                .AsSingle()
                .WithArguments(_settings)
                .NonLazy();
        }
    }
}