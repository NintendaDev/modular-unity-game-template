using System.Collections.Generic;
using UnityEngine;

namespace GameTemplate.Gameplay.GameObjects
{
    public class PatrolPointsRegistry : MonoBehaviour, IPatrolPoint
    {
        [SerializeField] 
        private Transform[] _patrolPoints;

        private Queue<Transform> _patrolPointsQueue;

        private void Awake()
        {
            _patrolPointsQueue = new Queue<Transform>(_patrolPoints);
            
            if (_patrolPointsQueue.Count == 0)
                _patrolPointsQueue.Enqueue(transform);
        }

        private void OnDrawGizmos()
        {
            if (_patrolPoints == null)
                return;

            float pointRadius = 0.2f;

            if (_patrolPoints.Length == 1)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(_patrolPoints[0].position, pointRadius);

                return;
            }

            for (int i = 0; i < _patrolPoints.Length - 1; i++)
            {
                Transform firstPoint = _patrolPoints[i];
                Transform secondPoint = _patrolPoints[i + 1];
                
                Gizmos.color = Color.green;
                Gizmos.DrawLine(firstPoint.position, secondPoint.position);
                
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(firstPoint.position, pointRadius);
                Gizmos.DrawSphere(secondPoint.position, pointRadius);
            }
        }

        public Transform GetNextPoint()
        {
            Transform nextPoint = _patrolPointsQueue.Dequeue();
            _patrolPointsQueue.Enqueue(nextPoint);

            return nextPoint;
        }
    }
}