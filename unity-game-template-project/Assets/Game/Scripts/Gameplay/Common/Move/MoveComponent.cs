using System;
using Modules.Conditions.Scripts;
using Modules.SimplePlatformer.Detectors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameTemplate.Gameplay.Common
{
    public abstract class MoveComponent : IMover
    {
        private readonly AndCondition _condition = new();
        private float _currentHorizontalDirection;
        
        protected MoveComponent(Settings settings)
        {
            MoveSettings = settings;
        }

        public event Action<Vector3> Moved;

        public bool IsForwardDirection => _currentHorizontalDirection > 0;

        public abstract Vector3 CurrentPosition { get; }

        protected Settings MoveSettings { get; private set; }

        public void AddMoveCondition(Func<bool> condition) => _condition.AddCondition(condition);

        public void RemoveMoveCondition(Func<bool> condition) => _condition.RemoveCondition(condition);

        public bool TryMove(Vector2 direction)
        {
            if (_condition.IsTrue() == false)
                return false;

            _currentHorizontalDirection = direction.x;

            bool isSuccess= TryMoveInternal(direction);
            
            if (isSuccess)
                Moved?.Invoke(direction);
            
            return isSuccess;
        }

        protected abstract bool TryMoveInternal(Vector2 direction);

        [Serializable]
        public class Settings
        {
            [SerializeField, MinValue(0)] private float _speed;

            public Settings(float speed, DetectorBehaviour wallDetector)
            {
                _speed = speed;
            }

            public float Speed => _speed;
        }
    }
}