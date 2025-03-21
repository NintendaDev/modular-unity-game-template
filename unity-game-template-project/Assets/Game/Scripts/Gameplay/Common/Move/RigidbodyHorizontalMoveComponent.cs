using Modules.SimplePlatformer.Detectors;
using UnityEngine;

namespace GameTemplate.Gameplay.Common
{
    public sealed class RigidbodyHorizontalMoveComponent : MoveComponent
    {
        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;
        private readonly DetectorBehaviour _wallDetector;

        public RigidbodyHorizontalMoveComponent(Rigidbody2D rigidbody, DetectorBehaviour wallDetector, 
            Settings settings) : base(settings)
        {
            _rigidbody = rigidbody;
            _transform = rigidbody.transform;
            _wallDetector = wallDetector;
        }
        
        public override Vector3 CurrentPosition => _transform.position;

        protected override bool TryMoveInternal(Vector2 direction)
        {
            if (HasWall())
                return false;
            
            Vector3 currentVelocity = _rigidbody.velocity;
            _rigidbody.velocity = new Vector3(direction.x * MoveSettings.Speed, currentVelocity.y, 
                currentVelocity.z);

            return true;
        }

        private bool HasWall() => _wallDetector.TryDetect(out _);
    }
}