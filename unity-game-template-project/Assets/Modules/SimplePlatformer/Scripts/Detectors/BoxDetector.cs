using UnityEngine;

namespace Modules.SimplePlatformer.Detectors
{
    public sealed class BoxDetector : DetectorBehaviour
    {
        [SerializeField] private Vector2 _size;
        
        private Collider2D[] _hits;
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(DetectorPoint.position, _size);
        }
        
        protected override void DetectInternal(ref Collider2D[] colliders)
        {
             Physics2D.OverlapBoxNonAlloc(DetectorPoint.position, _size, 0, colliders, DetectorLayerMask);
        }
    }
}