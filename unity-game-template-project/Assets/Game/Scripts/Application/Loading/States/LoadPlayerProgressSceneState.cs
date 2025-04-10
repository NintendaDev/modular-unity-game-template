using Cysharp.Threading.Tasks;
using Game.Application.Analytics;
using Game.Application.Common;
using Modules.Analytics;
using Modules.Analytics.Types;
using Modules.EventBus;
using Modules.Logging;
using Modules.SaveSystem.SaveLoad;

namespace Game.Application.Loading
{
    public sealed class LoadPlayerProgressSceneState : SceneState
    {
        private readonly IGameSaveLoader _gameSaveLoader;
        private readonly IDefaultSaveLoader _defaultSaveLoader;
        private readonly TemplateAnalyticsSystem _analyticsSystem;

        public LoadPlayerProgressSceneState(SceneStateMachine stateMachine, ISignalBus signalBus, ILogSystem logSystem,
            IGameSaveLoader gameSaveLoader, IDefaultSaveLoader defaultSaveLoader, 
            TemplateAnalyticsSystem analyticsSystem) 
            : base(stateMachine, signalBus, logSystem)
        {
            _gameSaveLoader = gameSaveLoader;
            _defaultSaveLoader = defaultSaveLoader;
            _analyticsSystem = analyticsSystem;
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            _analyticsSystem.SendGameBootEvent(GameBootStage.LoadProgress);

            if (await _gameSaveLoader.TryLoadAsync() == false)
                _defaultSaveLoader.LoadDefaultSave();

            await StateMachine.SwitchState<FinishLoadingSceneState>();
        }
    }
}
