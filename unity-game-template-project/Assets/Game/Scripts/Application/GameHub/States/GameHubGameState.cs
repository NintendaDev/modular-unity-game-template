using Cysharp.Threading.Tasks;
using Game.Application.Bootstrap;
using Game.Application.Common;
using Game.Application.LevelLoading;
using Modules.SceneManagement;
using Modules.LoadingCurtain;
using Modules.EventBus;
using Modules.Logging;

namespace Game.Application.GameHub
{
    public sealed class GameHubGameState : GameState
    {
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly ISingleSceneLoader _singleSceneLoader;
        private readonly GameLoadingAssetsConfiguration _gameLoadingAssetsConfiguration;

        public GameHubGameState(GameStateMachine stateMachine, ISignalBus signalBus, ILogSystem logSystem, 
            ILoadingCurtain loadingCurtain, ISingleSceneLoader singleSceneLoader, 
            IFastLoadInitialize levelLoaderInitializer, GameLoadingAssetsConfiguration gameLoadingAssetsConfiguration)
            : base(stateMachine, signalBus, logSystem)
        {
            _loadingCurtain = loadingCurtain;
            _singleSceneLoader = singleSceneLoader;
            _gameLoadingAssetsConfiguration = gameLoadingAssetsConfiguration;
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            _loadingCurtain.ShowWithoutProgressBar();
            await _singleSceneLoader.Load(_gameLoadingAssetsConfiguration.GameHubScene);
        }
    }
}
