using System;
using System.Collections.Generic;
using Modules.SimplePlatformer.Triggers;
using UnityEngine;
using Zenject;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class Snake : IDisposable, ITickable, IForceReceiver, ITossComponent
    {
        private readonly Settings _settings;
        private readonly ForceApplier _forceApplier;
        private readonly DamageApplyer _damageApplyer;
        private Transform _targetTransform;
        private readonly TriggerEventer _triggerEventer;
        private readonly List<Transform> _triggeredTransforms = new();
        private readonly UnitForceHandler _forceHandler;

        public Snake(Settings settings, IHealthComponent healthComponent, IMoveComponent moveComponent, TriggerEventer triggerEventer,
            Rigidbody2D rigidbody)
        {
            _triggerEventer = triggerEventer;
            _forceHandler = new UnitForceHandler(settings.ForceHandlerSettings, moveComponent, rigidbody);
            
            moveComponent.AddMoveCondition(() => healthComponent.IsAlive);

            _settings = settings;
            _forceApplier = new ForceApplier();
            _damageApplyer = new DamageApplyer();
            
            _triggerEventer.TriggerEnter += OnTriggerEnterEvent;
            _triggerEventer.TriggerExit += OnTriggerExitEvent;
        }

        public event Action Tossed;

        public void Dispose()
        {
            _triggerEventer.TriggerEnter -= OnTriggerEnterEvent;
            _triggerEventer.TriggerExit -= OnTriggerExitEvent;
        }

        public void Tick()
        {
            foreach (Transform transform in _triggeredTransforms.ToArray())
                HandleTriggeredTransform(transform);
        }

        public bool TryToss()
        {
            if (_targetTransform == null)
                return false;
            
            if (_forceApplier.TryApplyForForceReceiver(_targetTransform, Vector3.up, _settings.TossForce))
            {
                Tossed?.Invoke();
                
                return true;
            }

            return false;
        }

        public void ReceiveForce(float force, Vector3 direction, ForceMode2D mode) =>
            _forceHandler.Handle(force, direction, mode);

        private void HandleTriggeredTransform(Transform transform)
        {
            _targetTransform = transform;
            TryToss();
            _damageApplyer.TryApply(transform, _settings.Damage);
        }

        private void OnTriggerEnterEvent(Transform transform)
        {
            if (_triggeredTransforms.Contains(transform))
                return;
            
            _triggeredTransforms.Add(transform);
        }

        private void OnTriggerExitEvent(Transform transform)
        {
            if (_triggeredTransforms.Contains(transform) == false)
                return;
            
            _triggeredTransforms.Remove(transform);
        }

        [Serializable]
        public class Settings
        {
            public Settings(float tossForce, float damage, UnitForceHandler.Settings forceHandlerSettings)
            {
                TossForce = tossForce;
                Damage = damage;
                ForceHandlerSettings = forceHandlerSettings;
            }
            
            [field: SerializeField] public float TossForce { get; private set; }
            
            [field: SerializeField] public float Damage { get; private set; }

            [field: SerializeField] public UnitForceHandler.Settings ForceHandlerSettings { get; private set; }
        }
    }
}