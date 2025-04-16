using System;
using Modules.Conditions.Scripts;
using Modules.TimeUtilities.Timers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class JumpComponent : IDisposable, IJumpComponent
    {
        private readonly Settings _settings;
        private readonly Rigidbody2D _rigidbody;
        private readonly AndCondition _condition = new();
        private readonly CountdownTimer _cooldownTimer = new();

        public JumpComponent(Rigidbody2D rigidbody, Settings settings)
        {
            _rigidbody = rigidbody;
            _settings = settings;
            _condition.AddCondition(() => _cooldownTimer.IsRunning == false);
        }

        public event Action Jumped;

        public void Dispose()
        {
            _cooldownTimer.Dispose();
        }

        public void AddJumpCondition(Func<bool> condition) => _condition.AddCondition(condition);

        public void RemoveJumpCondition(Func<bool> condition) => _condition.RemoveCondition(condition);

        public void Jump()
        {
            if (_settings.Height < 0)
                throw new ArgumentException("Force must be positive");
            
            if (_settings.Height == 0 || _condition.IsTrue() == false)
                return;
            
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, CalculateVelocity());
            _cooldownTimer.Start(_settings.Cooldown);
            
            Jumped?.Invoke();
        }

        private float CalculateVelocity()
        {
            return Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y) * _settings.Height);
        }

        [Serializable]
        public class Settings
        {
            public Settings(float height, float cooldown)
            {
                Height = height;
                Cooldown = cooldown;
            }

            [MinValue(0)]
            [field: SerializeField] public float Height { get; private set; }

            [MinValue(0), Unit(Units.Second)]
            [field: SerializeField] public float Cooldown { get; private set; }
        }
    }
}