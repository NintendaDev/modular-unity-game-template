using System;
using GameTemplate.Gameplay.GameObjects;
using Modules.SimplePlatformer.Input;
using Zenject;

namespace Game.Gameplay.GameContext
{
    public sealed class PlayerInputController : IDisposable, IInitializable, IFixedTickable
    {
        private readonly IPlayerInput _playerInput;
        private readonly PlayerProvider _playerProvider;
        private ITossComponent _tossComponent;
        private IPushComponent _pushComponent;
        private IJumpComponent _jumpComponent;
        private IMoveComponent _moveComponent;

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
            _moveComponent = _playerProvider.Value.Get<IMoveComponent>();
        }

        public void FixedTick()
        {
            if (_playerInput.IsInitialized == false)
                return;
            
            _moveComponent.TryMove(_playerInput.ReadDirection());
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