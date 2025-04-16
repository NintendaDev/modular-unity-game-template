using System;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameTemplate.Gameplay.GameObjects
{
    public class PatrolComponent : IDisposable
    {
        protected const float ReachTargetThreshold = 0.1f;
        private readonly IPatrolPoint _patrolPoint;
        private readonly IMoveComponent _moveComponent;
        private readonly CompositeDisposable _disposable = new();
        
        [ShowInInspector, ReadOnly]
        private Transform _targetPoint;

        public PatrolComponent(IPatrolPoint patrolPoint, IMoveComponent moveComponent)
        {
            _patrolPoint = patrolPoint;
            _moveComponent = moveComponent;
            
            Observable.EveryUpdate(UnityFrameProvider.FixedUpdate)
                .Subscribe(_ => Update())
                .AddTo(_disposable);
        }
        
        protected Vector3 CurrentPosition => _moveComponent.CurrentPosition;
        
        protected Vector3 TargetPosition => _targetPoint.position;

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void Update()
        {
            if (_targetPoint == null)
                UpdateTarget();
            
            _moveComponent.TryMove(GetDirection());
            
            if (IsReachedTarget())
                UpdateTarget();
        }

        protected virtual Vector3 GetDirection() => (TargetPosition - CurrentPosition).normalized;

        protected virtual bool IsReachedTarget() =>
            Vector3.Distance(CurrentPosition, TargetPosition) <= ReachTargetThreshold;

        private void UpdateTarget() => _targetPoint = _patrolPoint.GetNextPoint();
    }
}