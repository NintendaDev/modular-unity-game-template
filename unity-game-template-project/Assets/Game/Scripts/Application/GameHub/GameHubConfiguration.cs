using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Application.GameHub
{
    [CreateAssetMenu(fileName = "new GameHubConfiguration", menuName = "GameTemplate/Infrastructure/GameHubConfiguration")]
    public sealed class GameHubConfiguration : ScriptableObject
    {
        [field: SerializeField, Required, AssetsOnly] 
        public AssetReferenceGameObject LevelViewPrebafReference { get; private set; }

        [field: SerializeField, Required, AssetsOnly] 
        public AssetReferenceGameObject WalletViewPrebafReference { get; private set; }
    }
}
