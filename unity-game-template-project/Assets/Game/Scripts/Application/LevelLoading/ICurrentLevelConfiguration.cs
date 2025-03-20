using Game.Application.Common;

namespace Game.Application.LevelLoading
{
    public interface ICurrentLevelConfiguration
    {
        public LevelConfiguration CurrentLevelConfiguration { get; }
    }
}