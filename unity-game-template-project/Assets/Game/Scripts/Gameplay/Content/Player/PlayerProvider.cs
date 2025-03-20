using Modules.Entities;

namespace GameTemplate.Gameplay.Content
{
    public sealed class PlayerProvider
    {
        public PlayerProvider(IEntity value)
        {
            Value = value;
        }

        public IEntity Value { get; }
    }
}