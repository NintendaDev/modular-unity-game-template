using System;

namespace GameTemplate.Gameplay.GameObjects
{
    public interface IPushComponent
    {
        public event Action Pushed;
        
        public bool TryPush();
    }
}