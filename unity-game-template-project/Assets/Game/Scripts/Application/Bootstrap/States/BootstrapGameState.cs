using Cysharp.Threading.Tasks;
using Game.Application.Common;
using Game.Application.LevelLoading;
using Game.Application.Loading;
using Modules.Analytics;
using Modules.AssetsManagement.StaticData;
using Modules.AudioManagement.Mixer;
using Modules.Device.Performance;
using Modules.Device.Performance.Configurations;
using Modules.EventBus;
using Modules.LoadingCurtain;
using Modules.Localization.Core.Systems;
using Modules.Logging;

namespace Game.Application.Bootstrap
{
    public sealed class BootstrapGameState : GameState
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ILevelLoaderService _gameLevelLoaderService;
        private readonly IAudioMixerSystem _audioMixerSystem;
        private readonly ILocalizationSystem _localizationSystem;
        private readonly IAnalyticsSystem _analyticsSystem;
        private readonly SystemPerformanceSetter _performanceSetter;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IPerformaceConfiguration _devicePerformaceConfigurator;

        public BootstrapGameState(GameStateMachine stateMachine, ISignalBus signalBus, ILogSystem logSystem, 
            IAnalyticsSystem analyticsSystem, IStaticDataService staticDataService, 
            ILevelLoaderService gameLevelLoaderService, IAudioMixerSystem audioMixerSystem, 
            IPerformaceConfiguration devicePerformaceConfigurator, ILocalizationSystem localizationSystem, 
            SystemPerformanceSetter performanceSetter, ILoadingCurtain loadingCurtain)
            : base(stateMachine, signalBus, logSystem)
        {
            _staticDataService = staticDataService;
            _gameLevelLoaderService = gameLevelLoaderService;
            _audioMixerSystem = audioMixerSystem;
            _localizationSystem = localizationSystem;
            _analyticsSystem = analyticsSystem;
            _performanceSetter = performanceSetter;
            _loadingCurtain = loadingCurtain;
            _devicePerformaceConfigurator = devicePerformaceConfigurator;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            
            _loadingCurtain.ShowWithoutProgressBar();

            await InitializeServices();
            await StateMachine.SwitchState<GameLoadingState>();
        }

        private async UniTask InitializeServices()
        {
            await _staticDataService.InitializeAsync();
            await _analyticsSystem.InitializeAsync();
            _devicePerformaceConfigurator.Initialize();
            _performanceSetter.Initialize();
            _gameLevelLoaderService.Initialize();
            _audioMixerSystem.Initialize();
            _localizationSystem.Initialize();
        }
    }
}