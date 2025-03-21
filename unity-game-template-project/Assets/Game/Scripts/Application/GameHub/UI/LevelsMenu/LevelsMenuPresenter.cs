using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Game.Application.Common;
using Modules.AssetsManagement.StaticData;

namespace Game.Application.GameHub.UI
{
    public sealed class LevelsMenuPresenter
    {
        private readonly LevelsMenuView _view;
        private readonly IStaticDataService _staticDataService;
        private readonly LevelViewFactory _levelViewFactory;
        private CancellationTokenSource _cancellationTokenSource;
        private LevelsConfigurationsHub _levelsConfigurationsHub;

        public LevelsMenuPresenter(LevelsMenuView view, IStaticDataService staticDataService, 
            LevelViewFactory levelViewFactory)
        {
            _view = view;
            _staticDataService = staticDataService;
            _levelViewFactory = levelViewFactory;

            UpdateLevelViewsAsync().Forget();
        }

        private async UniTask UpdateLevelViewsAsync()
        {
            if (_cancellationTokenSource != null)
                _cancellationTokenSource.Cancel();

            _cancellationTokenSource = new CancellationTokenSource();
            IEnumerable<LevelView> levelViews = await CreateLevelViewsAsync(_cancellationTokenSource.Token);
            _view.Link(levelViews);
        }

        private async UniTask<IEnumerable<LevelView>> CreateLevelViewsAsync(CancellationToken cancellationToken)
        {
            List<LevelView> levelViews = new();

            if (_levelsConfigurationsHub == null)
                _levelsConfigurationsHub = _staticDataService.GetConfiguration<LevelsConfigurationsHub>();

            foreach (LevelConfiguration levelConfiguration in _levelsConfigurationsHub.LevelsConfigurations)
            {
                if (cancellationToken.IsCancellationRequested)
                    return levelViews;

                levelViews.Add(await _levelViewFactory.CreateAsync(levelConfiguration));
            }

            return levelViews;
        }
    }
}
