using System;

namespace GameTemplate.Gameplay.GameObjects
{
    public interface IDamageable
    {
        public event Action Damaged;
        
        public bool CanTakeDamage { get; }
        
        public void TakeDamage(float damage);

        public void Kill();
    }
}