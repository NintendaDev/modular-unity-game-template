using Modules.Entities;
using Modules.SimplePlatformer.Triggers;
using Modules.SimplePlatformer.VFX;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameTemplate.Gameplay.GameObjects
{
    public class LavaInstaller : MonoInstaller<LavaInstaller>
    {
        [SerializeField, Required] private VisualEffect _destroyEffect;
        [SerializeField, Required] private Entity _entity;
        [SerializeField, Required] private TriggerEventer _triggerEventer;
        
        public override void InstallBindings()
        {
            Container.Bind<VisualEffect>().FromInstance(_destroyEffect).AsSingle();
            Container.Bind<TriggerEventer>().FromInstance(_triggerEventer).AsSingle();
            Container.BindInterfacesTo<Entity>().FromInstance(_entity).AsSingle();
            Container.BindInterfacesAndSelfTo<Lava>().AsSingle().NonLazy();
        }
    }
}