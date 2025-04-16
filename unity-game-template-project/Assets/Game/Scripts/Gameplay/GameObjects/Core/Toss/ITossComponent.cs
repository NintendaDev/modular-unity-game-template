using System;

namespace GameTemplate.Gameplay.GameObjects
{
    public interface ITossComponent
    {
        public event Action Tossed;
        
        public bool TryToss();
    }
}