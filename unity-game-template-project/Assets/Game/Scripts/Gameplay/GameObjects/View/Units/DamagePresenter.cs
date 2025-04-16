using System;
using Modules.SimplePlatformer.VFX;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class DamagePresenter : IDisposable
    {
        private readonly IDamageable _damageable;
        private readonly VisualEffect _effect;

        public DamagePresenter(IDamageable damageable, VisualEffect effect)
        {
            _damageable = damageable;
            _effect = effect;

            _damageable.Damaged += OnTakeDamage;
        }

        public void Dispose()
        {
            _damageable.Damaged -= OnTakeDamage;
        }

        private void OnTakeDamage()
        {
            _effect.Play();
        }
    }
}