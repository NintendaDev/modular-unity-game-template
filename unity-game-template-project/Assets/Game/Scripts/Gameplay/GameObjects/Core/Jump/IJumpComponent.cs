using System;

namespace GameTemplate.Gameplay.GameObjects
{
    public interface IJumpComponent
    {
        public event Action Jumped;
        
        public void AddJumpCondition(Func<bool> condition);

        public void RemoveJumpCondition(Func<bool> condition);
        
        public void Jump();
    }
}