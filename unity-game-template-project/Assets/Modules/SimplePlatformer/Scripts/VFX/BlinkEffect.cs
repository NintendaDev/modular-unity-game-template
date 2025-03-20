using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.SimplePlatformer.VFX
{
    public class BlinkEffect : TweenEffect
    {
        [SerializeField, Required] private SpriteRenderer[] _renderers;
        [SerializeField] private Color _blinkColor = Color.red;

        [SerializeField, MinValue(0)] private float _duration = 2f;
        
        [ValidateInput(nameof(IsCorrectBlinkSeconds))]
        [SerializeField, MinValue(0)] private float _frequency = 0.2f;
        
        private Color[] _originalColors;

        private void Awake()
        {
            _originalColors = new Color[_renderers.Length];

            for (int i = 0; i < _renderers.Length; i++)
                _originalColors[i] = _renderers[i].color;
        }

        protected override Tween CreateEffectTween()
        {
            Sequence sequence = DOTween.Sequence();
            
            int loopsCount = Mathf.FloorToInt(_duration / _frequency);

            for (int i = 0; i < _renderers.Length; i++)
            {
                Color originalColor = _originalColors[i];
                SpriteRenderer renderer = _renderers[i];
                
                sequence.Join(renderer
                    .DOColor(_blinkColor, _frequency).SetLoops(loopsCount, LoopType.Yoyo)
                    .OnComplete(() => renderer.color = originalColor)
                    .OnKill(() => renderer.color = originalColor));
            }

            return sequence;
        }
        
        private bool IsCorrectBlinkSeconds(float blinkSeconds, ref string errorMessage)
        {
            if (blinkSeconds > _duration)
            {
                errorMessage = "Blink Seconds greater than Effect Seconds";
                return false;
            }

            return true;
        }
    }
}