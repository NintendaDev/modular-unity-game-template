using System;
using Modules.SimplePlatformer.Triggers;
using UnityEngine;

namespace GameTemplate.Gameplay.Content
{
    public sealed class Platform : IDisposable
    {
        private readonly TriggerEventer _triggerEventer;
        private readonly Transform _transform;

        public Platform(TriggerEventer triggerEventer, Transform transform)
        {
            _triggerEventer = triggerEventer;
            _transform = transform;
            
            _triggerEventer.TriggerEnter += OnTriggerEnter;
            _triggerEventer.TriggerExit += OnTriggerExit;
        }

        public void Dispose()
        {
            _triggerEventer.TriggerEnter -= OnTriggerEnter;
            _triggerEventer.TriggerExit -= OnTriggerExit;
        }

        private void OnTriggerEnter(Transform transform)
        {
            if (transform.gameObject.activeInHierarchy)
                transform.SetParent(_transform);
        }

        private void OnTriggerExit(Transform transform)
        {
            if (transform.gameObject.activeInHierarchy)
                transform.SetParent(null);
        }
    }
}