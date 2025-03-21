using System;
using UnityEngine;

namespace Modules.SimplePlatformer.Triggers
{
    public class TriggerEventer : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask = ~0;

        public event Action<Transform> TriggerEnter;

        public event Action<Transform> TriggerExit;

        private void OnTriggerEnter2D(Collider2D other) => ProcessEnterCollision(other.transform);
        
        private void OnTriggerExit2D(Collider2D other) => ProcessExitCollision(other.transform);

        private void OnCollisionEnter2D(Collision2D other) => ProcessEnterCollision(other.transform);
        
        private void OnCollisionExit2D(Collision2D other) => ProcessExitCollision(other.transform);

        private void ProcessEnterCollision(Transform otherTransform)
        {
            if (CanProcess(otherTransform) == false)
                return;
            
            TriggerEnter?.Invoke(otherTransform);
        }

        private bool CanProcess(Transform otherTransform) => 
            (_layerMask & (1 << otherTransform.gameObject.layer)) != 0;

        private void ProcessExitCollision(Transform otherTransform)
        {
            if (CanProcess(otherTransform) == false)
                return;
            
            TriggerExit?.Invoke(otherTransform);
        } 
    }
}