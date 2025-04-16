using Modules.Entities;
using Modules.SimplePlatformer.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class PlatformInstaller : MonoInstaller<PlatformInstaller>
    {
        [SerializeField, Required] private Entity _entity;
        [SerializeField, Required] private TriggerEventer _triggerEventer;
        [SerializeField, Required] private PatrolPointsRegistry _movePoints;
        [SerializeField] private MoveComponent.Settings _moveSettings;

        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromInstance(transform).AsSingle();
            Container.BindInterfacesTo<Entity>().FromInstance(_entity).AsSingle();

            Container.BindInterfacesTo<TransformMoveComponent>()
                .AsSingle()
                .WithArguments(_moveSettings);
            
            Container.BindInterfacesAndSelfTo<PatrolComponent>()
                .AsSingle()
                .WithArguments(_movePoints)
                .NonLazy();
            
            Container.Bind<TriggerEventer>()
                .FromInstance(_triggerEventer)
                .AsSingle();
            
            Container.Bind<Platform>().AsSingle().NonLazy();
        }
    }
}