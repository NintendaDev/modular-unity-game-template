using System;
using Modules.SimplePlatformer.VFX;
using UnityEngine;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class PlayerTossPresenter : IDisposable
    {
        private readonly ITossComponent _tossComponent;
        private readonly Animator _animator;
        private readonly TossPresenter _tossPresenter;
        private readonly int _attackTriggerHash;

        public PlayerTossPresenter(ITossComponent tossComponent, VisualEffect effect, 
            Animator animator, string attackTriggerName)
        {
            _tossComponent = tossComponent;
            _animator = animator;
            _tossPresenter = new TossPresenter(_tossComponent, effect);
            _attackTriggerHash = Animator.StringToHash(attackTriggerName);
            
            tossComponent.Tossed += OnToss;
        }

        public void Dispose()
        {
            _tossComponent.Tossed -= OnToss;
            _tossPresenter.Dispose();
        }

        private void OnToss()
        {
            _animator.SetTrigger(_attackTriggerHash);
        }
    }
}