using System;
using Game.Gameplay.Common;
using Game.Gameplay.Common.Ground;
using Game.Gameplay.Common.Push;
using Game.Gameplay.Common.Toss;
using GameTemplate.Gameplay.Common;
using Modules.EventBus;
using Modules.SimplePlatformer.Detectors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameTemplate.Gameplay.Content
{
    public class Player : IDisposable, IDamageable, IJumpComponent, IMover, IPushComponent, ITossComponent, 
        IGroundDetector, IForceReceiver
    {
        private readonly Settings _settings;
        private readonly IHealthComponent _healthComponent;
        private readonly IMover _mover;
        private readonly JumpComponent _jumpComponent;
        private readonly Transform _transform;
        private readonly ISignalBus _signalBus;
        private readonly Rigidbody2D _rigidbody;
        private readonly ForceAbility _forceAbility;
        private readonly AutoDetector _groundDetector;
        private readonly ForceApplier _forceApplyer;

        public Player(Settings settings, IHealthComponent healthComponent, JumpComponent jumpComponent, 
            ForceAbility forceAbility, IMover mover, Transform transform, ISignalBus signalBus, Rigidbody2D rigidbody)
        {
            _settings = settings;
            _healthComponent = healthComponent;
            _jumpComponent = jumpComponent;
            _forceAbility = forceAbility;
            _mover = mover;
            _transform = transform;
            _signalBus = signalBus;
            _rigidbody = rigidbody;
            _groundDetector = settings.GroundDetector;
            _forceApplyer = new ForceApplier();

            _mover.AddMoveCondition(() => _healthComponent.IsAlive);
            _jumpComponent.AddJumpCondition(() => _healthComponent.IsAlive);
            _jumpComponent.AddJumpCondition(() => settings.GroundDetector.IsDetected);
            _forceAbility.AddCondition(() => _healthComponent.IsAlive);

            _healthComponent.Died += OnDie;
        }

        public event Action Pushed;

        public event Action Tossed;

        public event Action Damaged
        {
            add { _healthComponent.Damaged += value; }
            remove { _healthComponent.Damaged -= value; }
        }

        public event Action Jumped
        {
            add { _jumpComponent.Jumped += value; }
            remove { _jumpComponent.Jumped -= value; }
        }

        public event Action<Vector3> Moved
        {
            add { _mover.Moved += value; }
            remove { _mover.Moved -= value; }
        }

        public bool CanTakeDamage => _healthComponent.CanTakeDamage;

        public bool IsForwardDirection => _mover.IsForwardDirection;

        public Vector3 CurrentPosition => _mover.CurrentPosition;
        
        public bool IsDetectedGround => _groundDetector.IsDetected;

        public void Dispose()
        {
            _healthComponent.Died -= OnDie;
        }

        public void TakeDamage(float damage) => _healthComponent.TakeDamage(damage);

        public void Kill() => _healthComponent.Kill();
        
        public void AddJumpCondition(Func<bool> condition) => _jumpComponent.AddJumpCondition(condition);

        public void RemoveJumpCondition(Func<bool> condition) => _jumpComponent.RemoveJumpCondition(condition);

        public void Jump()
        {
            _transform.SetParent(null);
            _jumpComponent.Jump();
        }

        public void AddMoveCondition(Func<bool> condition) => _mover.AddMoveCondition(condition);

        public void RemoveMoveCondition(Func<bool> condition) => _mover.RemoveMoveCondition(condition);

        public bool TryMove(Vector2 direction) => _mover.TryMove(direction);
        
        public bool TryPush()
        {
            _forceAbility.ChangeDetector(_settings.PushDetector);

            if (_forceAbility.TryLaunch(_transform.right, _settings.PushForce))
            {
                Pushed?.Invoke();

                return true;
            }

            return false;
        }

        public bool TryToss()
        {
            if (_groundDetector.IsDetected == false)
                return false;
            
            _forceAbility.ChangeDetector(_settings.TossDetector);

            if (_forceAbility.TryLaunch(Vector3.up, _settings.TossForce))
            {
                Tossed?.Invoke();

                return true;
            }
            
            return false;
        }

        public void ReceiveForce(float force, Vector3 direction, ForceMode2D mode)
        {
            _forceApplyer.Apply(_rigidbody, force, direction, mode);
        }
            

        private void OnDie() => _signalBus.Invoke<PlayerDieSignal>();
        
        [Serializable]
        public class Settings
        {
            public Settings(AutoDetector groundDetector, DetectorBehaviour tossDetector, DetectorBehaviour pushDetector,
                float takeDamageCooldown, float pushForce, float tossForce)
            {
                GroundDetector = groundDetector;
                TossDetector = tossDetector;
                PushDetector = pushDetector;
                TakeDamageCooldown = takeDamageCooldown;
                PushForce = pushForce;
                TossForce = tossForce;
            }
            
            [field: SerializeField, Required] public AutoDetector GroundDetector { get; private set; }
            
            [field: SerializeField, Required] public DetectorBehaviour TossDetector { get; private set; }
            
            [field: SerializeField, Required] public DetectorBehaviour PushDetector { get; private set; }
            
            [field: SerializeField, MinValue(0)] public float TakeDamageCooldown { get; private set; }

            [field: SerializeField, MinValue(0)] public float PushForce { get; private set; }

            [field: SerializeField, MinValue(0)] public float TossForce { get; private set; }
        }
    }
}