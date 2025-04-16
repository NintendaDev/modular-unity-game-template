using System;
using Game.Gameplay.Common.Ground;
using Modules.EventBus;
using Modules.SimplePlatformer.Detectors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameTemplate.Gameplay.GameObjects
{
    public class Player : IDisposable, IPushComponent, ITossComponent, IGroundDetector, IForceReceiver
    {
        private readonly Settings _settings;
        private readonly Transform _transform;
        private readonly ISignalBus _signalBus;
        private readonly Rigidbody2D _rigidbody;
        private readonly ForceComponent _forceComponent;
        private readonly AutoDetector _groundDetector;
        private readonly ForceApplier _forceApplyer;
        private readonly IHealthComponent _healthComponent;
        private readonly JumpComponent _jumpComponent;

        public Player(Settings settings, IHealthComponent healthComponent, JumpComponent jumpComponent, 
            ForceComponent forceComponent, IMoveComponent moveComponent, Transform transform, ISignalBus signalBus, Rigidbody2D rigidbody)
        {
            _settings = settings;
            _healthComponent = healthComponent;
            _jumpComponent = jumpComponent;
            _forceComponent = forceComponent;
            _transform = transform;
            _signalBus = signalBus;
            _rigidbody = rigidbody;
            _groundDetector = settings.GroundDetector;
            _forceApplyer = new ForceApplier();

            moveComponent.AddMoveCondition(() => healthComponent.IsAlive);
            jumpComponent.AddJumpCondition(() => healthComponent.IsAlive);
            jumpComponent.AddJumpCondition(() => settings.GroundDetector.IsDetected);
            _forceComponent.AddCondition(() => healthComponent.IsAlive);

            _healthComponent.Died += OnDie;
            _jumpComponent.Jumped += OnJump;
        }

        public event Action Pushed;

        public event Action Tossed;

        public bool IsDetectedGround => _groundDetector.IsDetected;

        public void Dispose()
        {
            _healthComponent.Died -= OnDie;
            _jumpComponent.Jumped -= OnJump;
        }

        public bool TryPush()
        {
            _forceComponent.ChangeDetector(_settings.PushDetector);

            if (_forceComponent.TryLaunch(_transform.right, _settings.PushForce))
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
            
            _forceComponent.ChangeDetector(_settings.TossDetector);

            if (_forceComponent.TryLaunch(Vector3.up, _settings.TossForce))
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

        private void OnJump()
        {
            _transform.SetParent(null);
        }

        [Serializable]
        public class Settings
        {
            public Settings(AutoDetector groundDetector, DetectorBehaviour tossDetector, DetectorBehaviour pushDetector,
                float pushForce, float tossForce)
            {
                GroundDetector = groundDetector;
                TossDetector = tossDetector;
                PushDetector = pushDetector;
                PushForce = pushForce;
                TossForce = tossForce;
            }
            
            [field: SerializeField, Required] public AutoDetector GroundDetector { get; private set; }
            
            [field: SerializeField, Required] public DetectorBehaviour TossDetector { get; private set; }
            
            [field: SerializeField, Required] public DetectorBehaviour PushDetector { get; private set; }

            [field: SerializeField, MinValue(0)] public float PushForce { get; private set; }

            [field: SerializeField, MinValue(0)] public float TossForce { get; private set; }
        }
    }
}