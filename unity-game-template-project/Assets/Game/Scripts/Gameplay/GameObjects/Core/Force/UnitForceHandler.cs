using System;
using Modules.TimeUtilities.Timers;
using UnityEngine;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class UnitForceHandler : IDisposable
    {
        private readonly Settings _settings;
        private readonly Rigidbody2D _rigidbody;
        private readonly CountdownTimer _countdownTimer = new();
        private readonly ForceApplier _forceApplyer;

        public UnitForceHandler(Settings settings, IMoveComponent moveComponent, Rigidbody2D rigidbody)
        {
            _forceApplyer = new ForceApplier();
            _settings = settings;
            _rigidbody = rigidbody;
            moveComponent.AddMoveCondition(() => _countdownTimer.IsRunning == false);
        }

        public void Dispose()
        {
            _countdownTimer.Dispose();
        }

        public void Handle(float force, Vector3 direction, ForceMode2D mode)
        {
            _forceApplyer.Apply(_rigidbody, force, direction, mode);
            _countdownTimer.Start(_settings.MoveStopCooldown);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private float _moveStopCooldown;

            public Settings(float moveStopCooldown)
            {
                _moveStopCooldown = moveStopCooldown;
            }

            public float MoveStopCooldown => _moveStopCooldown;
        }
    }
}