using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.SimplePlatformer.VFX
{
    public abstract class VisualEffect : MonoBehaviour
    {
        [Button, HideInEditorMode]
        public abstract void Play();
    }
}