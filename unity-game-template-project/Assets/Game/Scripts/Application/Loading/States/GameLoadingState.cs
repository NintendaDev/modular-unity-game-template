using Cysharp.Threading.Tasks;
using Game.Application.Bootstrap;
using Game.Application.Common;
using Modules.SceneManagement;
using Modules.LoadingCurtain;
using Modules.EventBus;
using Modules.Logging;

namespace Game.Application.Loading
{
    public sealed class GameLoadingState : GameState
    {
        private readonly ISingleSceneLoader _singleSceneLoader;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly GameLoadingAssetsConfiguration _gameLoadingAssetsConfiguration;

        public GameLoadingState(GameStateMachine stateMachine, ISignalBus signalBus, ISingleSceneLoader singleSceneLoader, 
            ILoadingCurtain loadingCurtain, ILogSystem logSystem, 
            GameLoadingAssetsConfiguration gameLoadingAssetsConfiguration)
            : base(stateMachine, signalBus, logSystem)
        {
            _singleSceneLoader = singleSceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameLoadingAssetsConfiguration = gameLoadingAssetsConfiguration;
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            _loadingCurtain.ShowWithoutProgressBar();
            await _singleSceneLoader.Load(_gameLoadingAssetsConfiguration.GameLoadingScene);
        }
    }
}