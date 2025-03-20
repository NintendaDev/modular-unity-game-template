using GameTemplate.Gameplay.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameTemplate.Gameplay.Content
{
    public sealed class AutoMoveLavaInstaller : LavaInstaller
    {
        [SerializeField, Required] private Transform _transform;
        [SerializeField, Required] private PatrolPointsRegistry _patrolPoints;
        [SerializeField] private MoveComponent.Settings _moveSettings;
        
        public override void InstallBindings()
        {
            base.InstallBindings();
            
            Container.BindInterfacesTo<TransformMoveComponent>()
                .AsSingle()
                .WithArguments(_transform, _moveSettings);
            
            Container.BindInterfacesAndSelfTo<PatrolBehaviour>()
                .AsSingle()
                .WithArguments(_patrolPoints)
                .NonLazy();
        }
    }
}