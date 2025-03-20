using System;

namespace Game.Gameplay.Common.Toss
{
    public interface ITossComponent
    {
        public event Action Tossed;
        
        public bool TryToss();
    }
}