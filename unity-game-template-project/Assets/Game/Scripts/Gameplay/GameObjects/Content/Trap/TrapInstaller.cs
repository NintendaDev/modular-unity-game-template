using Modules.Entities;
using Modules.SimplePlatformer.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class TrapInstaller : MonoInstaller
    {
        [SerializeField, Required] private Entity _entity;
        [SerializeField, Required] private TriggerEventer _triggerEventer;
        [SerializeField] private Trap.Settings _settings;

        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromInstance(transform).AsSingle();
            Container.Bind<GameObject>().FromInstance(gameObject).AsSingle();
            
            Container.Bind<IHealthComponent>()
                .To<HealthComponent>()
                .AsSingle()
                .WithArguments(new HealthComponent.Settings(maxValue: 1, damageCooldown: 0));
            
            Container.Bind<TriggerEventer>()
                .FromInstance(_triggerEventer)
                .AsSingle();

            Container.BindInterfacesTo<Entity>()
                .FromInstance(_entity)
                .AsSingle();
            
            Container.BindInterfacesTo<Trap>()
                .AsSingle()
                .WithArguments(_settings)
                .NonLazy();

            InstalPresenters();
        }

        private void InstalPresenters()
        {
            Container.BindInterfacesTo<DiePresenter>()
                .AsSingle()
                .NonLazy();
        }
    }
}