using System;
using Game.Gameplay.Common.Toss;
using GameTemplate.Gameplay.Common;
using Modules.SimplePlatformer.Triggers;
using UnityEngine;

namespace GameTemplate.Gameplay.Content
{
    public sealed class Snake : Enemy, ITossComponent
    {
        private readonly Settings _settings;
        private readonly ForceApplier _forceApplier;
        private readonly DamageApplyer _damageApplyer;
        private Transform _targetTransform;

        public Snake(Settings settings, IHealthComponent healthComponent, IMover mover, TriggerEventer triggerEventer,
            Rigidbody2D rigidbody) 
            : base(healthComponent, mover, triggerEventer, settings.ForceHandlerSettings, rigidbody)
        {
            _settings = settings;
            _forceApplier = new ForceApplier();
            _damageApplyer = new DamageApplyer();
        }

        public event Action Tossed;

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

        protected override void HandleTriggeredTransform(Transform transform)
        {
            _targetTransform = transform;
            TryToss();
            _damageApplyer.TryApply(transform, _settings.Damage);
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