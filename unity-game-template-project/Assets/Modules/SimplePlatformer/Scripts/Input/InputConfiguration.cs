using UnityEngine;

namespace Modules.SimplePlatformer.Input
{
    [CreateAssetMenu(fileName = "new InputConfiguration", menuName = "Modules/SimplePlatformer/InputConfiguration")]
    public class InputConfiguration : ScriptableObject
    {
        [field: SerializeField] public string MoveActionName { get; private set; } = "Move";
        
        [field: SerializeField] public string JumpActionName { get; private set; } = "Jump";
        
        [field: SerializeField] public string PushActionName { get; private set; } = "Push";
        
        [field: SerializeField] public string TossActionName { get; private set; } = "Toss";
    }
}