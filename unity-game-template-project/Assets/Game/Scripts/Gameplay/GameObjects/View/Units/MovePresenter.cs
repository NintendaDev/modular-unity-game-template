using System;
using UnityEngine;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class MovePresenter : IDisposable
    {
        private const float ForwardYRotation = 0;
        private const float BackwardYRotation = 180;
        private readonly Transform _unitTransform;
        private LookDirection _lookDirection = LookDirection.Forward;
        private readonly IMoveComponent _moveComponent;

        public MovePresenter(Transform unitTransform, IMoveComponent moveComponent)
        {
            _unitTransform = unitTransform;
            _moveComponent = moveComponent;
            
            _moveComponent.Moved += OnMove;
        }

        private enum LookDirection { Forward, Backward }

        public void Dispose()
        {
            _moveComponent.Moved -= OnMove;
        }

        private void OnMove(Vector3 direction)
        {
            if (direction.x > 0)
                SetForwardLook();
            else if (direction.x < 0)
                SetBackwardLook();
        }

        private void SetForwardLook()
        {
            if (_lookDirection == LookDirection.Forward)
                return;
            
            _unitTransform.eulerAngles = CreateRotation(ForwardYRotation);
            _lookDirection = LookDirection.Forward;
        }

        private void SetBackwardLook()
        {
            if (_lookDirection == LookDirection.Backward)
                return;
            
            _unitTransform.eulerAngles = CreateRotation(BackwardYRotation);
            _lookDirection = LookDirection.Backward;
        }

        private Vector3 CreateRotation(float yRotation) =>
            new (_unitTransform.eulerAngles.x, yRotation, _unitTransform.eulerAngles.z);
    }
}