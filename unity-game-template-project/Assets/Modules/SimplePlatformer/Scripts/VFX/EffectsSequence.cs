using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Modules.SimplePlatformer.VFX
{
    public sealed class EffectsSequence : VisualEffect
    {
        [SerializeField, Required] private VisualEffect[] _effects;
        
        public override void Play()
        {
            _effects.ForEach(x => x.Play());
        }
    }
}