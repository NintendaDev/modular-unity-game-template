using System;
using Game.Gameplay.Common.Push;
using Modules.SimplePlatformer.VFX;

namespace Game.Gameplay.View.Units
{
    public sealed class PushPresenter : IDisposable
    {
        private readonly IPushComponent _pushComponent;
        private readonly VisualEffect _effect;

        public PushPresenter(IPushComponent pushComponent, VisualEffect effect)
        {
            _pushComponent = pushComponent;
            _effect = effect;

            _pushComponent.Pushed += OnPush;
        }

        public void Dispose()
        {
            _pushComponent.Pushed -= OnPush;
        }

        private void OnPush()
        {
            _effect.Play();
        }
    }
}