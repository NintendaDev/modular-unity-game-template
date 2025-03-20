using Game.Application.Common;

namespace Game.Application.LevelLoading
{
    public interface IFastLoadInitialize
    {
        public void InitializeFastLoad(LevelCode levelCode);
    }
}