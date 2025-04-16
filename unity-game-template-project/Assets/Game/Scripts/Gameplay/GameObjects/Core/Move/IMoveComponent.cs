using System;
using UnityEngine;

namespace GameTemplate.Gameplay.GameObjects
{
    public interface IMoveComponent
    {
        public event Action<Vector3> Moved;
        
        public bool IsForwardDirection { get; }

        public Vector3 CurrentPosition { get; }

        public void AddMoveCondition(Func<bool> condition);
        
        public void RemoveMoveCondition(Func<bool> condition);

        public bool TryMove(Vector2 direction);
    }
}