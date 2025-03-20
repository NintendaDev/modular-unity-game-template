using Game.Gameplay.View.Units;
using Modules.SimplePlatformer.VFX;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameTemplate.Gameplay.Content
{
    public sealed class SpiderInstaller : EnemyInstaller
    {
        [SerializeField, Required] private VisualEffect _pushEffect;
        
        [Title("Spider")]
        [SerializeField] private Spider.Settings _spiderSettings;
        
        public override void InstallBindings()
        {
            base.InstallBindings();

            Container.BindInterfacesAndSelfTo<Spider>()
                .AsSingle()
                .WithArguments(_spiderSettings)
                .NonLazy();
        }
        
        protected override void InstallPresenters()
        {
            base.InstallPresenters();
            
            Container.BindInterfacesTo<PushPresenter>()
                .AsSingle()
                .WithArguments(_pushEffect)
                .NonLazy();
        }
    }
}