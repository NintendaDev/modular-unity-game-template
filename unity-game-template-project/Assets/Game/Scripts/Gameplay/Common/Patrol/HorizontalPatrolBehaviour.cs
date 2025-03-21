using UnityEngine;

namespace GameTemplate.Gameplay.Common
{
    public sealed class HorizontalPatrolBehaviour : PatrolBehaviour
    {
        public HorizontalPatrolBehaviour(IPatrolPoint patrolPoint, IMover mover)
            : base(patrolPoint, mover)
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