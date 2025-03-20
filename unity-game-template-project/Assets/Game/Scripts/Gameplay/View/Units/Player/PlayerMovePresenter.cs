using Game.Gameplay.Common.Fall;
using Modules.SimplePlatformer.Input;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.View.Units
{
    public sealed class PlayerMovePresenter : MovePresenter, ITickable
    {
        private readonly IPlayerInput _playerInput;
        private readonly FallSpeedMeter _fallSpeedMeter;
        private readonly Animator _animator;
        private readonly int _isMovingParameterHash;
        private readonly int _speedYParameterHash;

        public PlayerMovePresenter(IPlayerInput playerInput, Transform unitTransform, FallSpeedMeter fallSpeedMeter, 
            Animator animator, string isMovingParameterName, string speedYParameterName) : base(unitTransform)
        {
            _playerInput = playerInput;
            _fallSpeedMeter = fallSpeedMeter;
            _animator = animator;
            _isMovingParameterHash = Animator.StringToHash(isMovingParameterName);
            _speedYParameterHash = Animator.StringToHash(speedYParameterName);
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
    }
}