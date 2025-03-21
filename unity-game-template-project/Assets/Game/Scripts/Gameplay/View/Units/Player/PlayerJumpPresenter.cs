using System;
using Game.Gameplay.Common.Ground;
using GameTemplate.Gameplay.Common;
using Modules.SimplePlatformer.VFX;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.View.Units
{
    public sealed class PlayerJumpPresenter : IDisposable, ITickable
    {
        private readonly IJumpComponent _jumpComponent;
        private readonly IGroundDetector _groundDetector;
        private readonly Animator _animator;
        private readonly JumpPresenter _jumpPresenter;
        private readonly int _jumpTriggerHash;
        private readonly int _isGroundParameterHash;

        public PlayerJumpPresenter(IJumpComponent jumpComponent, VisualEffect effect, IGroundDetector groundDetector,
            Animator animator, string jumpTriggerName, string isGroundParameterName)
        {
            _jumpComponent = jumpComponent;
            _groundDetector = groundDetector;
            _animator = animator;
            _jumpPresenter = new JumpPresenter(jumpComponent, effect);
            _jumpTriggerHash = Animator.StringToHash(jumpTriggerName);
            _isGroundParameterHash = Animator.StringToHash(isGroundParameterName);
            
            _jumpComponent.Jumped += OnJump;
        }

        public void Dispose()
        {
            _jumpComponent.Jumped -= OnJump;
            _jumpPresenter.Dispose();
        }

        public void Tick()
        {
            _animator.SetBool(_isGroundParameterHash, _groundDetector.IsDetectedGround);
        }

        private void OnJump()
        {
            _animator.SetTrigger(_jumpTriggerHash);
        }
    }
}