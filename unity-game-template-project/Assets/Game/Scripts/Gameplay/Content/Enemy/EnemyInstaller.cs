using Game.Gameplay.View.Units;
using GameTemplate.Gameplay.Common;
using Modules.Entities;
using Modules.SimplePlatformer.Detectors;
using Modules.SimplePlatformer.Triggers;
using Modules.SimplePlatformer.VFX;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameTemplate.Gameplay.Content
{
    public abstract class EnemyInstaller : MonoInstaller<EnemyInstaller>
    {
        [SerializeField, Required] private Entity _entity;
        [SerializeField, Required] private TriggerEventer _triggerEventer;
        [SerializeField, Required] private PatrolPointsRegistry _patrolPoints;
        [SerializeField, Required] private DetectorBehaviour _wallDetector;
        [SerializeField] private MoveComponent.Settings _moveSettings;
        [SerializeField] private HealthComponent.Settings _healthSettings;
        
        [Title("Visual Effects")]
        [SerializeField, Required] private VisualEffect _damageEffect;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Entity>().FromInstance(_entity).AsSingle();
            
            Container.BindInterfacesTo<RigidbodyHorizontalMoveComponent>()
                .AsSingle()
                .WithArguments(_wallDetector, _moveSettings);
            
            Container.BindInterfacesTo<HorizontalPatrolBehaviour>()
                .AsSingle()
                .WithArguments(_patrolPoints)
                .NonLazy();

            Container.Bind<IHealthComponent>()
                .To<HealthComponent>()
                .AsSingle()
                .WithArguments(_healthSettings);

            Container.Bind<TriggerEventer>()
                .FromInstance(_triggerEventer)
                .AsSingle();

            InstallPresenters();
        }
        
        protected virtual void InstallPresenters()
        {
            Container.BindInterfacesTo<DamagePresenter>()
                .AsSingle()
                .WithArguments(_damageEffect)
                .NonLazy();
            
            Container.BindInterfacesTo<EnemyMovePresenter>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesTo<DiePresenter>()
                .AsSingle()
                .WithArguments(gameObject)
                .NonLazy();
        }
    }
}