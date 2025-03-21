using System;
using Modules.TimeUtilities.Timers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameTemplate.Gameplay.Common
{
    public class HealthComponent : IHealthComponent
    {
        private readonly Settings _settings;
        private readonly CountdownTimer _cooldownTimer = new();
        private float _currentValue;

        public HealthComponent(Settings settings)
        {
            _settings = settings;
            CurrentValue = MaxValue;
        }

        public event Action Died;
        
        public event Action<float, float> Changed;

        public event Action Damaged;

        public bool CanTakeDamage => _cooldownTimer.IsRunning == false;
        
        [ShowInInspector, ReadOnly]
        public float MaxValue => _settings.MaxValue;
        
        [ShowInInspector, ReadOnly]
        public float CurrentValue
        {
            get => _currentValue;

            private set
            {
                float previousValue = _currentValue;
                
                _currentValue = Mathf.Clamp(value, 0f, MaxValue);

                if (_currentValue != previousValue)
                {
                    Changed?.Invoke(_currentValue, previousValue);
                    
                    if (_currentValue < previousValue)
                        Damaged?.Invoke();
                    
                    if (CurrentValue == 0)
                        Died?.Invoke();
                }
            }
        }
        
        [ShowInInspector, ReadOnly]
        public bool IsAlive => CurrentValue > 0;

        [Button]
        public void TakeDamage(float damage)
        {
            if (CanTakeDamage == false)
                return;
            
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage));

            if (IsAlive == false)
                return;
            
            CurrentValue -= damage;
            _cooldownTimer.Start(_settings.DamageCooldown);
        }

        public void Kill() => CurrentValue = 0;

        [Serializable]
        public class Settings
        {
            public Settings(float maxValue, float damageCooldown)
            {
                MaxValue = maxValue;
                DamageCooldown = damageCooldown;
            }

            [MinValue(0)]
            [field: SerializeField] public float MaxValue { get; private set; }

            [MinValue(0)]
            [field: SerializeField] public float DamageCooldown { get; private set; }
        }
    }
}


