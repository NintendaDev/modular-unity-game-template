using Modules.Entities;
using UnityEngine;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class ForceApplier
    {
        public bool TryApplyForForceReceiver(Transform transform, Vector3 direction, float force)
        {
            if (direction.x == 0 && direction.y == 0)
                return false;
            
            if (transform.TryGetComponent(out IEntity entity) == false)
                return false;

            if (entity.TryGet(out IForceReceiver forceReceiver) == false)
                return false;
            
            forceReceiver.ReceiveForce(force, direction, ForceMode2D.Impulse);

            return true;
        }
        
        public void Apply(Rigidbody2D rigidbody, float force, Vector3 direction, ForceMode2D mode)
        {
            Vector3 velocity = rigidbody.velocity;

            if (direction.x == 0)
                velocity.y = 0;
            else if (direction.y == 0)
                velocity.x = 0;
            else
                velocity = Vector3.zero;
            
            rigidbody.velocity = velocity;
            rigidbody.AddForce(direction * force * rigidbody.gravityScale, mode);
        }
    }
}