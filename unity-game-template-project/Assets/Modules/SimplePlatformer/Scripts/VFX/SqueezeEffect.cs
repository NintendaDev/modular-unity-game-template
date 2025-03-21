using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.SimplePlatformer.VFX
{
    public class SqueezeEffect : TweenEffect
    {
        [SerializeField, Required] private Transform _transformToSqueeze;
        [SerializeField, MinValue(0)] private float _duration = 2f;
        
        [ValidateInput(nameof(IsCorrectSqueezePercent))]
        [SerializeField] private Vector2 _squeezePercent = Vector2.one;
        
        private Vector3 _originalScale;

        private void Awake()
        {
            _originalScale = _transformToSqueeze.localScale;
        }

        protected override Tween CreateEffectTween()
        {
            Vector3 scale = _originalScale * _squeezePercent;
            scale.z = _originalScale.z;
            return _transformToSqueeze.DOScale(scale, _duration).SetLoops(2, LoopType.Yoyo);
        }

        private bool IsCorrectSqueezePercent(Vector2 squeezePercent, ref string errorMessage)
        {
            if (squeezePercent.x < 0 || squeezePercent.x > 1 || squeezePercent.y < 0 || squeezePercent.y > 1)
            {
                errorMessage = "Squeeze percent must be between 0 and 1 inclusive.";

                return false;
            }

            return true;
        }
    }
}