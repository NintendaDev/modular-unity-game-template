using Modules.LoadingCurtain;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Application.Bootstrap
{
    [CreateAssetMenu(fileName = "new LoadingCurtainInstaller", 
        menuName = "GameTemplate/Installers/LoadingCurtainInstaller")]
    public sealed class LoadingCurtainInstaller : ScriptableObjectInstaller<LoadingCurtainInstaller>
    {
        [SerializeField, Required, AssetsOnly] private LoadingCurtain _prefab;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<LoadingCurtain>()
                .FromComponentInNewPrefab(_prefab)
                .AsSingle()
                .OnInstantiated<LoadingCurtain>((_, curtain) => curtain.Hide());
        }
    }
}