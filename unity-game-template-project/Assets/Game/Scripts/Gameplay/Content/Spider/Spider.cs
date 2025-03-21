using System;
using Game.Gameplay.Common.Push;
using GameTemplate.Gameplay.Common;
using Modules.SimplePlatformer.Triggers;
using UnityEngine;

namespace GameTemplate.Gameplay.Content
{
    public sealed class Spider : Enemy, IPushComponent
    {
        private readonly Settings _settings;
        private readonly Transform _unitTransform;
        private readonly ForceApplier _forceApplier;
        private readonly DamageApplyer _damageApplyer;
        private Transform _targetTransform;

        public Spider(Settings settings, Transform unitTransform, IHealthComponent healthComponent, 
            IMover mover, TriggerEventer triggerEventer, Rigidbody2D rigidbody) 
            : base(healthComponent, mover, triggerEventer, settings.ForceHandlerSettings, rigidbody)
        {
            _settings = settings;
            _unitTransform = unitTransform;
            _forceApplier = new ForceApplier();
            _damageApplyer = new DamageApplyer();
        }

        public event Action Pushed;
        
        public bool TryPush()
        {
            if (_targetTransform == null)
                return false;
            
            if (_forceApplier.TryApplyForForceReceiver(_targetTransform, _unitTransform.right, _settings.PushForce))
            {
                Pushed?.Invoke();
                
                return true;
            }

            return false;
        }

        protected override void HandleTriggeredTransform(Transform transform)
        {
            _targetTransform = transform;
            
            _damageApplyer.TryApply(transform, _settings.Damage);
            TryPush();
        }

        [Serializable]
        public class Settings
        {
            public Settings(float pushForce, float damage, UnitForceHandler.Settings forceHandlerSettings)
            {
                PushForce = pushForce;
                Damage = damage;
                ForceHandlerSettings = forceHandlerSettings;
            }
            
            [field: SerializeField] public float PushForce { get; private set; }
            
            [field: SerializeField] public float Damage { get; private set; }
            
            [field: SerializeField] public UnitForceHandler.Settings ForceHandlerSettings { get; private set; }
        }
    }
}