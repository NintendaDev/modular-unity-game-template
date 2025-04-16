using Cysharp.Threading.Tasks;
using Game.Application.Analytics;
using Game.Application.Common;
using Game.Application.LevelLoading;
using Modules.LoadingCurtain;
using Modules.EventBus;
using Modules.Logging;

namespace Game.Gameplay.GameContext
{
    public sealed class GameplayGameState : GameState
    {
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IFastLoadLevel _levelLoader;
        private readonly TemplateAnalyticsSystem _analyticsSystem;

        public GameplayGameState(GameStateMachine stateMachine, ISignalBus signalBus, ILogSystem logSystem,
            ILoadingCurtain loadingCurtain, IFastLoadLevel levelLoader, TemplateAnalyticsSystem analyticsSystem) 
            : base(stateMachine, signalBus, logSystem)
        {
            _loadingCurtain = loadingCurtain;
            _levelLoader = levelLoader;
            _analyticsSystem = analyticsSystem;
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            _loadingCurtain.ShowWithoutProgressBar();
            
            _analyticsSystem.SendLevelLoadEvent(LevelBootStage.SceneLoad, _levelLoader.FastLoadLevelCode);
            await _levelLoader.FastLoadLevelAsync();
            
            _loadingCurtain.Hide();
        }
    }
}
