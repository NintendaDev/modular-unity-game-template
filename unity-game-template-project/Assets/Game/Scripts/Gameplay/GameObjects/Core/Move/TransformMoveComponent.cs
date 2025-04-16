using UnityEngine;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class TransformMoveComponent : MoveComponent
    {
        private readonly Transform _transform;

        public TransformMoveComponent(Transform transform, Settings settings) : base(settings)
        {
            _transform = transform;
        }
        
        public override Vector3 CurrentPosition => _transform.position;

        protected override bool TryMoveInternal(Vector2 direction)
        {
            Vector3 positionDelta = direction * Time.deltaTime * MoveSettings.Speed;
            positionDelta.z = 0;
            
            _transform.position += positionDelta;

            return true;
        }
    }
}