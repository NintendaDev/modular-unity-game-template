using System;
using Game.Application.Common;
using Game.Application.LevelLoading;
using UnityEngine;
using Zenject;

namespace Game.Application.Analytics
{
    public sealed class ProgressAnalyticsSender : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask = ~0;
        [SerializeField, Range(0, 100)] private int _progressPercent = 25;
        
        private TemplateAnalyticsSystem _analyticsSystem;
        private bool _isSended;
        private LevelCode _currentLevelCode;

        [Inject]
        private void Construct(TemplateAnalyticsSystem analyticsSystem, 
            ICurrentLevelConfiguration currentLevelConfiguration)
        {
            _analyticsSystem = analyticsSystem;
            _currentLevelCode = currentLevelConfiguration.CurrentLevelConfiguration.LevelCode;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            ProcessCollision(other);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            ProcessCollision(other);
        }

        private void ProcessCollision(Collider2D other)
        {
            if (_isSended)
                return;

            if ((_layerMask.value & (1 << other.gameObject.layer)) == 0)
                return;
            
            _analyticsSystem.SendLevelProgressEvent(_currentLevelCode, _progressPercent);
            _isSended = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + new Vector3(0, 100, 0), transform.position - new Vector3(0, 100, 0));
        }
    }
}