using System;

namespace GameTemplate.Gameplay.Common
{
    public interface IHealthComponent : IDamageable
    {
        public event Action Died;
        
        public event Action<float, float> Changed;
        
        public float MaxValue { get; }
        
        public float CurrentValue { get; }
        
        public bool IsAlive { get; }
    }
}