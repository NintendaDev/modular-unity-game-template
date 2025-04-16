using System;
using Modules.SimplePlatformer.VFX;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class JumpPresenter : IDisposable
    {
        private readonly IJumpComponent _jumpComponent;
        private readonly VisualEffect _jumpEffect;

        public JumpPresenter(IJumpComponent jumpComponent, VisualEffect jumpEffect)
        {
            _jumpComponent = jumpComponent;
            _jumpEffect = jumpEffect;

            _jumpComponent.Jumped += OnJump;
        }

        public void Dispose()
        {
            _jumpComponent.Jumped -= OnJump;
        }

        private void OnJump()
        {
            _jumpEffect.Play();
        }
    }
}