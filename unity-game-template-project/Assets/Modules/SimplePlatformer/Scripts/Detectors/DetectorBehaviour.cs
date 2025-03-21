using System.Collections.Generic;
using System.Linq;
using Modules.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.SimplePlatformer.Detectors
{
    public abstract class DetectorBehaviour : MonoBehaviour
    {
        [SerializeField, MinValue(1)] private int _maxDetectedColliders = 10;
        [SerializeField, Required] private Transform _detectorPoint;
        [SerializeField] private LayerMask _detectorLayerMask = ~0;
        
        private Collider2D[] _detectedColliders;
        
        public Transform DetectorPoint => _detectorPoint;

        protected LayerMask DetectorLayerMask => _detectorLayerMask;
        
        private void Awake()
        {
            _detectedColliders = new Collider2D[_maxDetectedColliders];
        }
        
        public bool TryDetect(out IEnumerable<Collider2D> colliders)
        {
            ClearDetectedColliders();
            DetectInternal(ref _detectedColliders);
            colliders = _detectedColliders;

            return colliders.Any(x => x != null);
        }

        protected abstract void DetectInternal(ref Collider2D[] colliders);

        private void ClearDetectedColliders()
        {
            for (int i = 0; i < _detectedColliders.Length; ++i)
                _detectedColliders[i] = null;
        }
    }
}