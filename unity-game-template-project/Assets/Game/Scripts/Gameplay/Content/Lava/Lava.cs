using System;
using GameTemplate.Gameplay.Common;
using Modules.SimplePlatformer.Triggers;
using Modules.SimplePlatformer.VFX;
using UnityEngine;

namespace GameTemplate.Gameplay.Content
{
    public class Lava : IDisposable
    {
        private readonly VisualEffect _destroyEffect;
        private readonly TriggerEventer _autoDieTrigger;
        private readonly DamageApplyer _damageApplyer = new();

        public Lava(TriggerEventer triggerEventer, VisualEffect destroyEffect)
        {
            _autoDieTrigger = triggerEventer;
            _destroyEffect = destroyEffect;
            
            _autoDieTrigger.TriggerEnter += OnTriggerEnter;
        }

        public void Dispose()
        {
            _autoDieTrigger.TriggerEnter -= OnTriggerEnter;
        }

        private void OnTriggerEnter(Transform transform)
        {
            _damageApplyer.TryKill(transform);
            _destroyEffect?.Play();
        }
    }
}