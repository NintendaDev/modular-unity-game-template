using Cysharp.Threading.Tasks;
using Modules.LoadingCurtain;
using System.Collections.Generic;
using Game.Application.Common;
using Game.Application.LevelLoading;
using UnityEngine;
using Modules.Analytics;
using Modules.AudioManagement.Player;
using Modules.AudioManagement.Types;
using Modules.Core.Systems;
using Modules.EventBus;
using Modules.Logging;

namespace Game.Gameplay.States
{
    public abstract class LevelGameplayState : SceneState
    {
        private readonly float _originalTimeScale;
        private readonly IEnumerable<IReset> _resetObjects;
        private readonly ILoadingCurtain _loadingCurtain;

        public LevelGameplayState(SceneStateMachine stateMachine, ISignalBus signalBus, ILogSystem logSystem,
            IAnalyticsSystem analyticsSystem, IAudioAssetPlayer audioAssetPlayer, IEnumerable<IReset> resetObjects, 
            ILoadingCurtain loadingCurtain, ICurrentLevelConfiguration levelConfigurator) 
            : base(stateMachine, signalBus, logSystem)
        {
            AudioAssetPlayer = audioAssetPlayer;
            _originalTimeScale = Time.timeScale;
            _resetObjects = resetObjects;
            _loadingCurtain = loadingCurtain;
            CurrentLevelConfiguration = levelConfigurator.CurrentLevelConfiguration;
            AnalyticsSystem = analyticsSystem;
        }

        protected IAudioAssetPlayer AudioAssetPlayer { get; private set; }

        protected LevelConfiguration CurrentLevelConfiguration { get; private set; }

        protected IAnalyticsSystem AnalyticsSystem { get; private set; }

        protected void ResetGameplay()
        {
            foreach (IReset resetObject in _resetObjects)
                resetObject.Reset();
        }

        protected void StopGameTime() =>
            Time.timeScale = 0;

        protected void RestoreGameTime() =>
            Time.timeScale = _originalTimeScale;

        protected void PlayOrUnpauseSound(AudioCode audioCode) =>
            AudioAssetPlayer.TryPlayAsync(audioCode).Forget();

        protected void PauseAllSounds() =>
            AudioAssetPlayer.PauseAll();
        
        protected void UnpauseAllSounds() =>
            AudioAssetPlayer.UnpauseAll();

        protected void ShowCurtain() =>
            _loadingCurtain.ShowWithoutProgressBar();

        protected void HideCurtain() =>
            _loadingCurtain.Hide();

        protected async UniTask SwitchStartState() =>
            await StateMachine.SwitchState<StartGameplaySceneState>();

        protected async UniTask SwitchPlayState() =>
            await StateMachine.SwitchState<PlayGameplaySceneState>();

        protected async UniTask SwitchPauseState() =>
            await StateMachine.SwitchState<PauseGameplaySceneState>();

        protected async UniTask SwitchFinishState() =>
            await StateMachine.SwitchState<FinishGameplaySceneState>();
    }
}
