using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.SimplePlatformer.Detectors
{
    public sealed class AutoDetector : MonoBehaviour
    {
        [SerializeField, Required] private DetectorBehaviour[] _detectors;

        public event Action Detected;
        
        [ShowInInspector, ReadOnly, HideInEditorMode]
        public bool IsDetected { get; private set; }

        private void FixedUpdate()
        {
            StartDetectBehaviour();
        }

        private void StartDetectBehaviour()
        {
            foreach (DetectorBehaviour detector in _detectors)
            {
                if (detector.TryDetect(out _))
                {
                    if (IsDetected == false)
                        Detected?.Invoke();
                    
                    IsDetected = true;

                    return;
                }
            }

            IsDetected = false;
        }
    }
}