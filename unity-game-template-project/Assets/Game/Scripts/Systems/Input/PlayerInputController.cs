using System;
using Game.Gameplay.Common.Push;
using Game.Gameplay.Common.Toss;
using GameTemplate.Gameplay.Common;
using GameTemplate.Gameplay.Content;
using Modules.SimplePlatformer.Input;
using Zenject;

namespace GameTemplate.Systems.Input
{
    public sealed class PlayerInputController : IDisposable, IInitializable, IFixedTickable
    {
        private readonly IPlayerInput _playerInput;
        private readonly PlayerProvider _playerProvider;
        private ITossComponent _tossComponent;
        private IPushComponent _pushComponent;
        private IJumpComponent _jumpComponent;
        private IMover _mover;

        public PlayerInputController(IPlayerInput playerInput, PlayerProvider playerProvider)
        {
            _playerInput = playerInput;
            _playerProvider = playerProvider;

            _playerInput.JumpPressed += OnJumpPress;
            _playerInput.PushPressed += OnPushPress;
            _playerInput.TossPressed += OnTossPress;
        }

        public void Dispose()
        {
            _playerInput.JumpPressed -= OnJumpPress;
            _playerInput.PushPressed -= OnPushPress;
            _playerInput.TossPressed -= OnTossPress;
        }

        public void Initialize()
        {
            _tossComponent = _playerProvider.Value.Get<ITossComponent>();
            _pushComponent = _playerProvider.Value.Get<IPushComponent>();
            _jumpComponent = _playerProvider.Value.Get<IJumpComponent>();
            _mover = _playerProvider.Value.Get<IMover>();
        }

        public void FixedTick()
        {
            if (_playerInput.IsInitialized == false)
                return;
            
            _mover.TryMove(_playerInput.ReadDirection());
        }

        private void OnJumpPress()
        {
            _jumpComponent.Jump();
        }

        private void OnPushPress()
        {
            _pushComponent.TryPush();
        }

        private void OnTossPress()
        {
            _tossComponent.TryToss();
        }
    }
}