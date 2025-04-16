using Modules.SimplePlatformer.VFX;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameTemplate.Gameplay.GameObjects
{
    public sealed class SnakeInstaller : EnemyInstaller
    {
        [SerializeField, Required] private VisualEffect _tossEffect;
        
        [Title("Snake")]
        [SerializeField] private Snake.Settings _snakeSettings;
        
        public override void InstallBindings()
        {
            base.InstallBindings();
            
            Container.BindInterfacesAndSelfTo<Snake>()
                .AsSingle()
                .WithArguments(_snakeSettings)
                .NonLazy();
        }

        protected override void InstallPresenters()
        {
            base.InstallPresenters();
            
            Container.BindInterfacesTo<TossPresenter>()
                .AsSingle()
                .WithArguments(_tossEffect)
                .NonLazy();
        }
    }
}