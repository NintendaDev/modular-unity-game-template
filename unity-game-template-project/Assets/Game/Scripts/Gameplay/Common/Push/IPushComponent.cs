using System;

namespace Game.Gameplay.Common.Push
{
    public interface IPushComponent
    {
        public event Action Pushed;
        
        public bool TryPush();
    }
}