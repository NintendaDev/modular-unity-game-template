using System;
using GameTemplate.Gameplay.Common;
using UnityEngine;

namespace Game.Gameplay.View.Units
{
    public sealed class EnemyMovePresenter : MovePresenter, IDisposable
    {
        private readonly IMover _mover;

        public EnemyMovePresenter(Transform unitTransform, IMover mover) 
            : base(unitTransform)
        {
            _mover = mover;
            
            _mover.Moved += OnMove;
        }

        public void Dispose()
        {
            _mover.Moved -= OnMove;
        }
    }
}