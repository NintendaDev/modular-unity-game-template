namespace Modules.Conditions.Scripts
{
    public sealed class SequenceCondition : ICondition
    {
        private readonly ICondition[] _conditions;

        public SequenceCondition(params ICondition[] conditions)
        {
            _conditions = conditions;
        }
            
        public bool IsTrue()
        {
            foreach (ICondition condition in _conditions)
                if (condition.IsTrue())
                    return true;
            
            return false;
        }
    }
}