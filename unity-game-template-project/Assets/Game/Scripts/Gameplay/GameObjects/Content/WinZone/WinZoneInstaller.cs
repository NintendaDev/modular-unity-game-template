using Modules.Entities;
using Modules.SimplePlatformer.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Content
{
    public sealed class WinZoneInstaller : MonoInstaller<WinZoneInstaller>
    {
        [SerializeField, Required] private Entity _entity;
        [SerializeField, Required] private TriggerEventer _triggerEventer;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Entity>().FromInstance(_entity).AsSingle();
            
            Container.Bind<TriggerEventer>()
                .FromInstance(_triggerEventer)
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<WinZone>().AsSingle().NonLazy();
        }
    }
}