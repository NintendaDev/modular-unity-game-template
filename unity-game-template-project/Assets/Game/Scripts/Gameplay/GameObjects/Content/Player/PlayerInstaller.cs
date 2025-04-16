using Game.Gameplay.Common;
using Game.Gameplay.Common.Fall;
using Modules.Entities;
using Modules.SimplePlatformer.Detectors;
using Modules.SimplePlatformer.VFX;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        [SerializeField, Required, ChildGameObjectsOnly] private Entity _entity;
        [SerializeField] private JumpComponent.Settings _jumperSettings;
        [SerializeField] private Player.Settings _playerSettings;
        [SerializeField, Required, ChildGameObjectsOnly] private DetectorBehaviour _wallDetector;
        [SerializeField] private MoveComponent.Settings _moveSettings;
        [SerializeField] private ForceComponent.Settings _forceAbilitySettings;
        [SerializeField] private FallSpeedLimiter.Settings _fallSpeedLimiterSettings;
        [SerializeField] private HealthComponent.Settings _healthSettings;
        
        [Title("Animation")]
        [SerializeField, Required, ChildGameObjectsOnly] private Animator _animator;
        [SerializeField, Required] private string _isMovingParameterName = "IsMoving";
        [SerializeField, Required] private string _jumpTriggerName = "Jump";
        [SerializeField, Required] private string _speedYParameterName = "SpeedY";
        [SerializeField, Required] private string _attackTriggerName = "Attack";
        [SerializeField, Required] private string _isGroundParameterName = "IsGround";

        [Title("Visual Effects")]
        [SerializeField, Required, ChildGameObjectsOnly] private VisualEffect _damageEffect;
        [SerializeField, Required, ChildGameObjectsOnly] private VisualEffect _jumpEffect;
        [SerializeField, Required, ChildGameObjectsOnly] private VisualEffect _pushEffect;
        [SerializeField, Required, ChildGameObjectsOnly] private VisualEffect _tossEffect;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Entity>().FromInstance(_entity).AsSingle();
            
            Container.BindInterfacesTo<HealthComponent>()
                .AsSingle()
                .WithArguments(_healthSettings);

            Container.BindInterfacesTo<FallSpeedLimiter>()
                .AsSingle()
                .WithArguments(_fallSpeedLimiterSettings);
            
            InstallAbilities();
            InstallPlayer();
            InstallPresenters();
        }

        private void InstallAbilities()
        {
            Container.BindInterfacesTo<RigidbodyHorizontalMoveComponent>()
                .AsSingle()
                .WithArguments(_moveSettings, _wallDetector);
            
            Container.BindInterfacesAndSelfTo<JumpComponent>()
                .AsSingle()
                .WithArguments(_jumperSettings);

            Container.BindInterfacesAndSelfTo<ForceComponent>()
                .AsCached()
                .WithArguments(_forceAbilitySettings, _playerSettings.PushDetector)
                .WhenInjectedInto<Player>();
        }

        private void InstallPlayer()
        {
            Container.BindInterfacesAndSelfTo<Player>()
                .AsSingle()
                .WithArguments(_playerSettings);
        }

        private void InstallPresenters()
        {
            Container.BindInterfacesAndSelfTo<FallSpeedMeter>().AsSingle();
            
            Container.BindInterfacesTo<DamagePresenter>()
                .AsSingle()
                .WithArguments(_damageEffect)
                .NonLazy();
            
            Container.BindInterfacesTo<DiePresenter>()
                .AsSingle()
                .WithArguments(gameObject)
                .NonLazy();
            
            Container.BindInterfacesTo<PlayerJumpPresenter>()
                .AsSingle()
                .WithArguments(_jumpEffect, _animator, _jumpTriggerName, _isGroundParameterName)
                .NonLazy();

            Container.BindInterfacesTo<PlayerMovePresenter>()
                .AsSingle()
                .WithArguments(_animator, _isMovingParameterName, _speedYParameterName)
                .NonLazy();
            
            Container.BindInterfacesTo<PlayerPushPresenter>()
                .AsSingle()
                .WithArguments(_pushEffect, _animator, _attackTriggerName)
                .NonLazy();
            
            Container.BindInterfacesTo<PlayerTossPresenter>()
                .AsSingle()
                .WithArguments(_tossEffect, _animator, _attackTriggerName)
                .NonLazy();
        }
    }
}