using Cysharp.Threading.Tasks;
using Game.Application.Common;
using Modules.Analytics;
using Modules.Analytics.Types;
using Modules.EventBus;
using Modules.Logging;
using Modules.SaveSystem.SaveLoad;

namespace Game.Application.Loading
{
    public sealed class LoadPlayerProgressSceneState : AnalyticsSceneState
    {
        private readonly IGameSaveLoader _gameSaveLoader;
        private readonly IDefaultSaveLoader _defaultSaveLoader;

        public LoadPlayerProgressSceneState(SceneStateMachine stateMachine, ISignalBus signalBus, ILogSystem logSystem,
            IGameSaveLoader gameSaveLoader, IDefaultSaveLoader defaultSaveLoader, IAnalyticsSystem analyticsSystem) 
            : base(stateMachine, signalBus, logSystem, analyticsSystem)
        {
            _gameSaveLoader = gameSaveLoader;
            _defaultSaveLoader = defaultSaveLoader;
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            SendAnalyticsEvent(AnalyticsEventCode.GameBootLoadProgress);

            if (await _gameSaveLoader.TryLoadAsync() == false)
                _defaultSaveLoader.LoadDefaultSave();

            await StateMachine.SwitchState<FinishLoadingSceneState>();
        }
    }
}
