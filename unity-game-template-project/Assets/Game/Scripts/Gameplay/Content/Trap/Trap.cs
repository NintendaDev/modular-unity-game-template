using System;
using GameTemplate.Gameplay.Common;
using Modules.Entities;
using Modules.SimplePlatformer.Triggers;
using UnityEngine;

namespace GameTemplate.Gameplay.Content
{
    public class Trap : IDisposable, IDamageable, IForceReceiver
    {
        private readonly Settings _settings;
        private readonly TriggerEventer _triggerEventer;
        private readonly IHealthComponent _healthComponent;
        private readonly Rigidbody2D _rigidbody;
        private readonly ForceApplier _forceApplier;

        public Trap(Settings settings, TriggerEventer triggerEventer, IHealthComponent healthComponent,
            Rigidbody2D rigidbody)
        {
            _settings = settings;
            _triggerEventer = triggerEventer;
            _healthComponent = healthComponent;
            _rigidbody = rigidbody;
            _forceApplier = new ForceApplier();

            _triggerEventer.TriggerEnter += OnTriggerEnter;
        }

        public event Action Damaged
        {
            add { _healthComponent.Damaged += value; }
            remove { _healthComponent.Damaged -= value; }
        }

        public void Dispose()
        {
            _triggerEventer.TriggerEnter -= OnTriggerEnter;
        }

        public bool CanTakeDamage => true;

        public void TakeDamage(float damage)
        {
            _healthComponent.Kill();
        }

        public void Kill() => _healthComponent.Kill();

        private void OnTriggerEnter(Transform transform)
        {
            if (transform.TryGetComponent(out IEntity entity) == false)
                return;

            if (entity.TryGet(out IDamageable damageable) == false)
                return;
            
            damageable.TakeDamage(_settings.Damage);
            Kill();
        }

        public void ReceiveForce(float force, Vector3 direction, ForceMode2D mode)
        {
            _forceApplier.Apply(_rigidbody, force, direction, mode);
        }

        [Serializable]
        public class Settings
        {
            public Settings(float damage)
            {
                Damage = damage;
            }
            
            [field: SerializeField] public float Damage { get; private set; }
        }
    }
}