using Cysharp.Threading.Tasks;
using Game.Application.Common;

namespace Game.Application.LevelLoading
{
    public interface IFastLoadLevel
    {
        public LevelCode FastLoadLevelCode { get; }
        
        public UniTask FastLoadLevelAsync();
    }
}