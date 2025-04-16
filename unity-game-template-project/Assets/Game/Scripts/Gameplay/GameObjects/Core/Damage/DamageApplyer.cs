using Modules.Entities;
using UnityEngine;

namespace GameTemplate.Gameplay.GameObjects
{
    public class DamageApplyer
    {
        public bool TryApply(Transform transform, float damage)
        {
            if (TryGetDamageable(transform, out IDamageable damageable) == false)
                return false;
            
            damageable.TakeDamage(damage);

            return true;
        }

        public bool TryKill(Transform transform)
        {
            if (TryGetDamageable(transform, out IDamageable damageable) == false)
                return false;
            
            damageable.Kill();

            return true;
        }

        private bool TryGetDamageable(Transform transform, out IDamageable damageable)
        {
            damageable = null;
            
            if (transform.TryGetComponent(out Entity entity) == false)
                return false;

            return entity.TryGet(out damageable);
        }
    }
}