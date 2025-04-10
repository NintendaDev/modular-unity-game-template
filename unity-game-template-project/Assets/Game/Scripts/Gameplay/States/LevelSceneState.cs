using Cysharp.Threading.Tasks;
using Modules.LoadingCurtain;
using System.Collections.Generic;
using Game.Application.Analytics;
using Game.Application.Common;
using Game.Application.LevelLoading;
using UnityEngine;
using Modules.AudioManagement.Player;
using Modules.AudioManagement.Types;
using Modules.Core.Systems;
using Modules.EventBus;
using Modules.Logging;

namespace Game.Gameplay.States
{
    public abstract class LevelSceneState : SceneState
    {
        private readonly float _originalTimeScale;
        private readonly IEnumerable<IReset> _resetObjects;
        private readonly ILoadingCurtain _loadingCurtain;

        protected LevelSceneState(SceneStateMachine stateMachine, ISignalBus signalBus, ILogSystem logSystem,
            TemplateAnalyticsSystem analyticsSystem, IAudioAssetPlayer audioAssetPlayer, IEnumerable<IReset> resetObjects, 
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
        
        protected LevelConfiguration CurrentLevelConfiguration { get; private set; }
        
        protected TemplateAnalyticsSystem AnalyticsSystem { get; private set; }
        
        private IAudioAssetPlayer AudioAssetPlayer { get; }

        protected void ResetGameplay()
        {
            foreach (IReset resetObject in _resetObjects)
                resetObject.Reset();
        }

        protected void StopGameTime() =>
            Time.timeScale = 0;

        protected void RestoreGameTime() =>
            Time.timeScale = _originalTimeScale;

        protected void PlayOrUnpauseSound(AudioCode audioCode) => AudioAssetPlayer.TryPlayAsync(audioCode).Forget();

        protected void PauseAllSounds() => AudioAssetPlayer.PauseAll();
        
        protected void UnpauseAllSounds() => AudioAssetPlayer.UnpauseAll();

        protected void ShowCurtain() => _loadingCurtain.ShowWithoutProgressBar();

        protected void HideCurtain() => _loadingCurtain.Hide();

        protected async UniTask SwitchPlayState() => await StateMachine.SwitchState<PlaySceneState>();

        protected async UniTask SwitchPauseState() => await StateMachine.SwitchState<PauseSceneState>();

        protected async UniTask SwitchFinishState() => await StateMachine.SwitchState<FinishSceneState>();
        
        protected async UniTask SwitchGameOverState() => await StateMachine.SwitchState<GameOverSceneState>();
    }
}
