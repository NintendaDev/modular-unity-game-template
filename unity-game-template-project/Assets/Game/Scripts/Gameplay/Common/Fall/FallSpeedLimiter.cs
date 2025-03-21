using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Common
{
    public sealed class FallSpeedLimiter : IFixedTickable
    {
        private readonly Settings _settings;
        private readonly Rigidbody2D _rigidbody;

        public FallSpeedLimiter(Settings settings, Rigidbody2D rigidbody)
        {
            _settings = settings;
            _rigidbody = rigidbody;
        }

        public void FixedTick()
        {
            if (-_rigidbody.velocity.y <= _settings.MaxValue)
                return;
            
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, -_settings.MaxValue);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField, MinValue(0)] private float _maxValue;
            
            public Settings(float maxValue = 20)
            {
                _maxValue = maxValue;
            }

            public float MaxValue => _maxValue;
        }
    }
}