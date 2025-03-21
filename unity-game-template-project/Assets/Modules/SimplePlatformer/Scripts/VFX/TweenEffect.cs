using DG.Tweening;

namespace Modules.SimplePlatformer.VFX
{
    public abstract class TweenEffect : VisualEffect
    {
        private Tween _tween;
        
        public override void Play()
        {
            KillEffect();
            _tween = CreateEffectTween();
        }

        protected abstract Tween CreateEffectTween();

        private void KillEffect()
        {
            if (_tween != null)
            {
                _tween.Kill(complete: true);
                _tween = null;
            }
        }
    }
}