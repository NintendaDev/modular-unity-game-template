using System;
using Modules.SimplePlatformer.Triggers;
using Modules.SimplePlatformer.VFX;
using UnityEngine;

namespace GameTemplate.Gameplay.GameObjects
{
    public class Trampoline : IDisposable
    {
        private readonly Settings _settings;
        private readonly Transform _transform;
        private readonly TriggerEventer _triggerEventer;
        private readonly VisualEffect _launchEffect;
        private readonly ForceApplier _forceApplier = new();

        public Trampoline(Settings settings, Transform transform, TriggerEventer triggerEventer,
            VisualEffect launchEffect)
        {
            _settings = settings;
            _transform = transform;
            _triggerEventer = triggerEventer;
            _launchEffect = launchEffect;

            _triggerEventer.TriggerEnter += OnTriggerEnter;
        }

        public void Dispose()
        {
            _triggerEventer.TriggerEnter -= OnTriggerEnter;
        }

        private void OnTriggerEnter(Transform transform)
        {
            if (_forceApplier.TryApplyForForceReceiver(transform, _transform.up, _settings.Force))
                _launchEffect.Play();
        }

        [Serializable]
        public class Settings
        {
            public Settings(float force)
            {
                Force = force;
            }

            [field: SerializeField] public float Force { get; private set; }
        }
    }
}