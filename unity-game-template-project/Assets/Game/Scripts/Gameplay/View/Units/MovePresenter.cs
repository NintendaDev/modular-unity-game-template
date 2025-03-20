using UnityEngine;

namespace Game.Gameplay.View.Units
{
    public abstract class MovePresenter
    {
        private const float ForwardYRotation = 0;
        private const float BackwardYRotation = 180;
        private readonly Transform _unitTransform;
        private LookDirection _lookDirection = LookDirection.Forward;

        protected MovePresenter(Transform unitTransform)
        {
            _unitTransform = unitTransform;
        }

        private enum LookDirection { Forward, Backward }

        protected void OnMove(Vector3 direction)
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