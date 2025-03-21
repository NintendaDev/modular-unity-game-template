using UnityEngine;
using Zenject;

namespace Game.Gameplay.Common.Fall
{
    public sealed class FallSpeedMeter : ITickable
    {
        private readonly Transform _transform;
        private float _lastYPosition;

        public FallSpeedMeter(Transform transform)
        {
            _transform = transform;
        }

        public float Value { get; private set; }

        public void Tick()
        {
            UpdateFallSpeed();
        }

        private void UpdateFallSpeed()
        {
            float currentPositionY = _transform.position.y;
            Value = (currentPositionY - _lastYPosition) / Time.deltaTime;
            _lastYPosition = currentPositionY;
        }
    }
}