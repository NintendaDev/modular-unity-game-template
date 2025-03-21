using System;
using GameTemplate.Gameplay.Content;
using Modules.EventBus;
using Modules.SimplePlatformer.Triggers;
using UnityEngine;

namespace Game.Gameplay.Content
{
    public sealed class WinZone : IDisposable
    {
        private readonly TriggerEventer _triggerEventer;
        private readonly ISignalBus _signalBus;

        public WinZone(TriggerEventer triggerEventer, ISignalBus signalBus)
        {
            _triggerEventer = triggerEventer;
            _signalBus = signalBus;
            _triggerEventer.TriggerEnter += OnTriggerEnter;
        }

        public void Dispose()
        {
            _triggerEventer.TriggerEnter -= OnTriggerEnter;
        }

        private void OnTriggerEnter(Transform player)
        {
            Debug.Log("============ Player Win!");
            _signalBus.Invoke<PlayerWinSignal>();
        }
    }
}