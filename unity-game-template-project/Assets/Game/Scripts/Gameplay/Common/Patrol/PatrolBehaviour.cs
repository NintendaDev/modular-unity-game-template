using System;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameTemplate.Gameplay.Common
{
    public class PatrolBehaviour : IDisposable
    {
        protected const float ReachTargetThreshold = 0.1f;
        private readonly IPatrolPoint _patrolPoint;
        private readonly IMover _mover;
        private readonly CompositeDisposable _disposable = new();
        
        [ShowInInspector, ReadOnly]
        private Transform _targetPoint;

        public PatrolBehaviour(IPatrolPoint patrolPoint, IMover mover)
        {
            _patrolPoint = patrolPoint;
            _mover = mover;
            
            Observable.EveryUpdate(UnityFrameProvider.FixedUpdate)
                .Subscribe(_ => Update())
                .AddTo(_disposable);
        }
        
        protected Vector3 CurrentPosition => _mover.CurrentPosition;
        
        protected Vector3 TargetPosition => _targetPoint.position;

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void Update()
        {
            if (_targetPoint == null)
                UpdateTarget();
            
            _mover.TryMove(GetDirection());
            
            if (IsReachedTarget())
                UpdateTarget();
        }

        protected virtual Vector3 GetDirection() => (TargetPosition - CurrentPosition).normalized;

        protected virtual bool IsReachedTarget() =>
            Vector3.Distance(CurrentPosition, TargetPosition) <= ReachTargetThreshold;

        private void UpdateTarget() => _targetPoint = _patrolPoint.GetNextPoint();
    }
}