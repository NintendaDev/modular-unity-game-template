using Modules.Entities;

namespace Game.Gameplay.GameContext
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