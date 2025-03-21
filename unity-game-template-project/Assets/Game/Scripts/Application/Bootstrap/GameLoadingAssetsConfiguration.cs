using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Application.Bootstrap
{
    [CreateAssetMenu(fileName = "new GameLoadingAssetsConfiguration",
        menuName = "GameTemplate/Infrastructure/GameLoadingAssetsConfiguration")]
    public sealed class GameLoadingAssetsConfiguration : ScriptableObject
    {
        [field: SerializeField, Required] 
        public AssetReference Curtain { get; private set; }

        [field: SerializeField, Required] 
        public AssetReference GameHubScene { get; private set; }

        [field: SerializeField, Required] 
        public AssetReference GameLoadingScene { get; private set; }

        [field: SerializeField, Required] 
        public string GameBootstraperResourcePath { get; private set; } = "Infrastructure/GameBootstrapper";
    }
}
