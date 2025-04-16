using System;
using System.Collections.Generic;
using Modules.Conditions.Scripts;
using Modules.SimplePlatformer.Detectors;
using Modules.TimeUtilities.Timers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class ForceComponent : IDisposable
    {
        private readonly Settings _settings;
        private readonly CountdownTimer _cooldownTimer = new();
        private readonly AndCondition _condition = new();
        private readonly ForceApplier _forceApplier = new();
        private DetectorBehaviour _detector;

        public ForceComponent(Settings settings, DetectorBehaviour detector)
        {
            _settings = settings;
            _detector = detector;
            _condition.AddCondition(() => _cooldownTimer.IsRunning == false);
        }

        public void Dispose()
        {
            _cooldownTimer.Dispose();
        }

        public void AddCondition(Func<bool> condition) => _condition.AddCondition(condition);

        public void RemoveCondition(Func<bool> condition) => _condition.RemoveCondition(condition);

        public void ChangeDetector(DetectorBehaviour detector)
        {
            _detector = detector;
        }

        public bool TryLaunch(Vector2 direction, float force)
        {
            if (force < 0)
                throw new ArgumentException("Force must be positive");
            
            if (force == 0 || _condition.IsTrue() == false)
                return false;

            if (_detector.TryDetect(out IEnumerable<Collider2D> colliders))
            {
                foreach (Collider2D collider in colliders)
                {
                    if (collider == null)
                        continue;
                    
                    _forceApplier.TryApplyForForceReceiver(collider.transform, direction, force);
                }
            }
                
            _cooldownTimer.Start(_settings.Cooldown);

            return true;
        }

        [Serializable]
        public class Settings
        {
            public Settings(float cooldown)
            {
                Cooldown = cooldown;
            }

            [MinValue(0), Unit(Units.Second)]
            [field: SerializeField] public float Cooldown { get; private set; }
        }
    }
}