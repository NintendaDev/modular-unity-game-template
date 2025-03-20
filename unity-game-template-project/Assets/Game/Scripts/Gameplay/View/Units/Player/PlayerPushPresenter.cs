using System;
using Game.Gameplay.Common.Push;
using Modules.SimplePlatformer.VFX;
using UnityEngine;

namespace Game.Gameplay.View.Units
{
    public sealed class PlayerPushPresenter : IDisposable
    {
        private readonly IPushComponent _pushComponent;
        private readonly Animator _animator;
        private readonly PushPresenter _pushPresenter;
        private readonly int _attackTriggerHash;

        public PlayerPushPresenter(IPushComponent pushComponent, VisualEffect effect, 
            Animator animator, string attackTriggerName)
        {
            _pushComponent = pushComponent;
            _animator = animator;
            _pushPresenter = new PushPresenter(pushComponent, effect);
            _attackTriggerHash = Animator.StringToHash(attackTriggerName);
            
            _pushComponent.Pushed += OnPush;
        }

        public void Dispose()
        {
            _pushComponent.Pushed -= OnPush;
            _pushPresenter.Dispose();
        }

        private void OnPush()
        {
            _animator.SetTrigger(_attackTriggerHash);
        }
    }
}