using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Modules.PopupsSystem.Configurations
{
    [CreateAssetMenu(fileName = "new PopupsAssetsConfiguration", 
        menuName = "Modules/PopupsSystem/PopupsAssetsConfiguration")]
    public sealed class PopupsAssetsConfiguration : ScriptableObject
    {
        [field: SerializeField, Required] public AssetReference InfoPopup { get; private set; }

        [field: SerializeField, Required] public AssetReference ErrorPopup { get; private set; }
    }
}
