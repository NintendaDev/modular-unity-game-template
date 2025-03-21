using Cysharp.Threading.Tasks;
using Game.Application.Common;

namespace Game.Application.LevelLoading
{
    public interface ILevelLoaderService : IFastLoadLevel, IFastLoadInitialize, ICurrentLevelConfiguration
    {
        public void Initialize();

        public UniTask LoadLevelAsync(LevelCode levelCode);
    }
}