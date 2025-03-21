using System;
using Game.Gameplay.Common.Toss;
using Modules.SimplePlatformer.VFX;

namespace Game.Gameplay.View.Units
{
    public sealed class TossPresenter : IDisposable
    {
        private readonly ITossComponent _tossComponent;
        private readonly VisualEffect _effect;

        public TossPresenter(ITossComponent tossComponent, VisualEffect effect)
        {
            _tossComponent = tossComponent;
            _effect = effect;

            _tossComponent.Tossed += OnToss;
        }

        public void Dispose()
        {
            _tossComponent.Tossed -= OnToss;
        }

        private void OnToss()
        {
            _effect.Play();
        }
    }
}