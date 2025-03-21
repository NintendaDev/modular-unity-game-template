using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Modules.SimplePlatformer.Input
{
    public interface IPlayerInput
    {
        public event Action JumpPressed;
        
        public event Action PushPressed;
        
        public event Action TossPressed;

        public bool IsInitialized { get; }
        
        public UniTask InitializeAsync();

        public Vector2 ReadDirection();
    }
}