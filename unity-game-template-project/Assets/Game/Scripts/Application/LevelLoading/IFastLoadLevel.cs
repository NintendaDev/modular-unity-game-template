using Cysharp.Threading.Tasks;

namespace Game.Application.LevelLoading
{
    public interface IFastLoadLevel
    {
        public UniTask FastLoadLevelAsync();
    }
}