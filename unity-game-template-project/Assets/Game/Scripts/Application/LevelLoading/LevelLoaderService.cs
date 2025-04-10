using Cysharp.Threading.Tasks;
using Game.Application.Common;
using Modules.SceneManagement;
using Modules.AssetsManagement.StaticData;

namespace Game.Application.LevelLoading
{
    public sealed class LevelLoaderService : ILevelLoaderService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ISingleSceneLoader _singleSceneLoader;
        private LevelCode _levelCodeForFastLoading;
        private LevelsConfigurationsHub _levelsConfigurations;

        public LevelLoaderService(IStaticDataService staticDataService, ISingleSceneLoader singleSceneLoader)
        {
            _staticDataService = staticDataService;
            _singleSceneLoader = singleSceneLoader;
        }

        public LevelConfiguration CurrentLevelConfiguration { get; private set; }

        public LevelCode FastLoadLevelCode => _levelCodeForFastLoading;

        public async UniTask FastLoadLevelAsync() =>
            await LoadLevelAsync(_levelCodeForFastLoading);

        public void Initialize()
        {
            _levelsConfigurations = _staticDataService.GetConfiguration<LevelsConfigurationsHub>();
        }

        public async UniTask LoadLevelAsync(LevelCode levelCode)
        {
            if (_levelsConfigurations.TryGetLevelConfiguration(levelCode,
                    out LevelConfiguration levelConfiguration) == false)
            {
                throw new System.Exception($"Level configuration with code {levelCode} was not found");
            }
            
            CurrentLevelConfiguration = levelConfiguration;

            await _singleSceneLoader.Load(levelConfiguration.SceneAddress);
        }

        public void InitializeFastLoad(LevelCode levelCode)
        {
            _levelCodeForFastLoading = levelCode;
        }
    }
}
