using System;
using GameTemplate.Gameplay.Common;
using UnityEngine;

namespace Game.Gameplay.View.Units
{
    public sealed class DiePresenter : IDisposable
    {
        private readonly IHealthComponent _healthComponent;
        private readonly GameObject _unitGameObject;

        public DiePresenter(IHealthComponent healthComponent, GameObject unitGameObject)
        {
            _healthComponent = healthComponent;
            _unitGameObject = unitGameObject;

            _healthComponent.Died += OnDie;
        }

        public void Dispose()
        {
            _healthComponent.Died -= OnDie;
        }

        private void OnDie()
        {
            _unitGameObject.SetActive(false);
        }
    }
}