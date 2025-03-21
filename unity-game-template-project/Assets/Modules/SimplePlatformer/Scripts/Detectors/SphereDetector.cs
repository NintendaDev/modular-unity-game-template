using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.SimplePlatformer.Detectors
{
    public sealed class SphereDetector : DetectorBehaviour
    {
        [SerializeField, MinValue(0)] private float _radius;
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(DetectorPoint.position, _radius);
        }

        protected override void DetectInternal(ref Collider2D[] colliders)
        {
            Physics2D.OverlapCircleNonAlloc(DetectorPoint.position, _radius, colliders, DetectorLayerMask);
        }
    }
}