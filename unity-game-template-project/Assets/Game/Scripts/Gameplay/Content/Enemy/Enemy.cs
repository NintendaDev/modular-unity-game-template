using System;
using System.Collections.Generic;
using GameTemplate.Gameplay.Common;
using Modules.SimplePlatformer.Triggers;
using UnityEngine;
using Zenject;

namespace GameTemplate.Gameplay.Content
{
    public abstract class Enemy : IDisposable, IDamageable, IForceReceiver, ITickable
    {
        private readonly IHealthComponent _healthComponent;
        private readonly IMover _mover;
        private readonly TriggerEventer _triggerEventer;
        private readonly List<Transform> _triggeredTransforms = new();
        private readonly UnitForceHandler _forceHandler;

        protected Enemy(IHealthComponent healthComponent, IMover mover, TriggerEventer triggerEventer,
            UnitForceHandler.Settings forceHandlerSettings, Rigidbody2D rigidbody)
        {
            _mover = mover;
            _healthComponent = healthComponent;
            _triggerEventer = triggerEventer;
            _forceHandler = new UnitForceHandler(forceHandlerSettings, mover, rigidbody);
            
            _mover.AddMoveCondition(() => _healthComponent.IsAlive);
            _triggerEventer.TriggerEnter += OnTriggerEnterEvent;
            _triggerEventer.TriggerExit += OnTriggerExitEvent;
        }

        public event Action Damaged
        {
            add { _healthComponent.Damaged += value; }
            remove { _healthComponent.Damaged -= value; }
        }
        
        public bool CanTakeDamage => _healthComponent.CanTakeDamage;

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

        public void AddMoveCondition(Func<bool> condition) => _mover.AddMoveCondition(condition);

        public void TakeDamage(float damage) => _healthComponent.TakeDamage(damage);

        public void Kill() => _healthComponent.Kill();

        public void ReceiveForce(float force, Vector3 direction, ForceMode2D mode) =>
            _forceHandler.Handle(force, direction, mode);

        protected abstract void HandleTriggeredTransform(Transform transform);

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
    }
}