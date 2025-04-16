using UnityEngine;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class HorizontalPatrolComponent : PatrolComponent
    {
        public HorizontalPatrolComponent(IPatrolPoint patrolPoint, IMoveComponent moveComponent)
            : base(patrolPoint, moveComponent)
        {
        }

        protected override Vector3 GetDirection()
        {
            Vector3 target = TargetPosition;
            target.y = CurrentPosition.y;

            return (target - CurrentPosition).normalized;
        }

        protected override bool IsReachedTarget() => 
            Mathf.Abs(CurrentPosition.x - TargetPosition.x) <= ReachTargetThreshold;
    }
}