using System;
using Game.Gameplay.Common.Fall;
using Modules.SimplePlatformer.Input;
using UnityEngine;
using Zenject;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class PlayerMovePresenter : IDisposable, ITickable
    {
        private const float ForwardYRotation = 0;
        private const float BackwardYRotation = 180;
        private readonly IPlayerInput _playerInput;
        private readonly Transform _unitTransform;
        private readonly FallSpeedMeter _fallSpeedMeter;
        private readonly IMoveComponent _moveComponent;
        private readonly Animator _animator;
        private readonly int _isMovingParameterHash;
        private readonly int _speedYParameterHash;
        private LookDirection _lookDirection = LookDirection.Forward;

        public PlayerMovePresenter(IPlayerInput playerInput, Transform unitTransform, FallSpeedMeter fallSpeedMeter, 
            IMoveComponent moveComponent, Animator animator, string isMovingParameterName, string speedYParameterName)
        {
            _playerInput = playerInput;
            _unitTransform = unitTransform;
            _fallSpeedMeter = fallSpeedMeter;
            _moveComponent = moveComponent;
            _animator = animator;
            _isMovingParameterHash = Animator.StringToHash(isMovingParameterName);
            _speedYParameterHash = Animator.StringToHash(speedYParameterName);
            
            _moveComponent.Moved += OnMove;
        }
        private enum LookDirection { Forward, Backward }

        public void Dispose()
        {
            _moveComponent.Moved -= OnMove;
        }

        public void Tick()
        {
            if (_playerInput.IsInitialized == false)
                return;
            
            Vector2 currentDirection = _playerInput.ReadDirection();
            OnMove(currentDirection);
            _animator.SetBool(_isMovingParameterHash, currentDirection.sqrMagnitude > 0);
            _animator.SetFloat(_speedYParameterHash, _fallSpeedMeter.Value);
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