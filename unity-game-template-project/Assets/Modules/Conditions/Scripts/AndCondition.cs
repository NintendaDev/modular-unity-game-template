using System;
using System.Collections.Generic;

namespace Modules.Conditions.Scripts
{
    public sealed class AndCondition : ICondition
    {
        private readonly List<Func<bool>> _conditions = new();

        public void AddCondition(Func<bool> condition) => _conditions.Add(condition);
        
        public void RemoveCondition(Func<bool> condition) => _conditions.Remove(condition);

        public bool IsTrue()
        {
            foreach (Func<bool> condition in _conditions)
                if (condition.Invoke() == false)
                    return false;
            
            return true;
        }
    }
}