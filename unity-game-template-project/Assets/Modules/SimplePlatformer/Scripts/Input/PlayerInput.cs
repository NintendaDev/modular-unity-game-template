using System;
using Cysharp.Threading.Tasks;
using Modules.AssetsManagement.StaticData;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Modules.SimplePlatformer.Input
{
    public sealed class PlayerInput : IDisposable, IPlayerInput
    {
        private readonly IStaticDataService _staticDataService;
        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _pushAction;
        private InputAction _tossAction;

        public PlayerInput(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }
        
        public bool IsInitialized { get; private set; }

        public UniTask InitializeAsync()
        {
            if (IsInitialized)
                return UniTask.CompletedTask;
            
            InputConfiguration configuration = _staticDataService.GetConfiguration<InputConfiguration>();
            
            _moveAction = InputSystem.actions.FindAction(configuration.MoveActionName);
            _jumpAction = InputSystem.actions.FindAction(configuration.JumpActionName);
            _pushAction = InputSystem.actions.FindAction(configuration.PushActionName);
            _tossAction = InputSystem.actions.FindAction(configuration.TossActionName);
            
            _jumpAction.performed += OnJumpPress;
            _pushAction.performed += OnPushPress;
            _tossAction.performed += OnTossPress;

            IsInitialized = true;
            
            return UniTask.CompletedTask;
        }

        public event Action JumpPressed;

        public event Action PushPressed;
        
        public event Action TossPressed;

        public void Dispose()
        {
            _jumpAction.performed -= OnJumpPress;
            _pushAction.performed -= OnPushPress;
            _tossAction.performed -= OnTossPress;
        }

        public Vector2 ReadDirection() => new(_moveAction.ReadValue<float>(), 0);

        private void OnJumpPress(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                JumpPressed?.Invoke();
        }

        private void OnPushPress(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                PushPressed?.Invoke();
        }

        private void OnTossPress(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                TossPressed?.Invoke();
        }
    }
}