using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using Game.Application.Common;
using Game.Application.GameHub;
using Game.UI.GameHub.LevelsMenu.Presenters;
using Game.UI.GameHub.LevelsMenu.Views;
using Modules.AssetsManagement.AddressablesOperations;
using Modules.AssetsManagement.StaticData;
using Modules.Core.Systems;
using Modules.EventBus;
using Modules.Localization.Core.Processors;
using Modules.ObjectsManagement.Factories;
using Zenject;

namespace Game.UI.GameHub.LevelsMenu.Factories
{
    public sealed class LevelViewFactory : IDisposable
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ISignalBus _signalBus;
        private readonly LocalizedTermProcessorLinker _localizedTermProcessorLinker;
        private readonly DictionaryDatabase<LevelView, Action> _destroyCallbacks = new();
        private readonly PrefabFactoryAsync<LevelView> _prefabFactory;
        private GameHubConfiguration _gameHubConfiguration;
        private List<IDisposable> _disposableObjects = new();

        public LevelViewFactory(IInstantiator instantiator, IAddressablesService addressablesService,
            IStaticDataService staticDataService, ISignalBus signalBus,
            LocalizedTermProcessorLinker localizedTermProcessorLinker)
        {
            _staticDataService = staticDataService;
            _signalBus = signalBus;
            _prefabFactory = new PrefabFactoryAsync<LevelView>(instantiator, addressablesService);
            _localizedTermProcessorLinker = localizedTermProcessorLinker;
        }

        public void Dispose()
        {
            _disposableObjects.ForEach(x => x.Dispose());
            _prefabFactory.Dispose();
        }

        public async UniTask<LevelView> CreateAsync(LevelConfiguration levelConfiguration)
        {
            if (_gameHubConfiguration == null)
                _gameHubConfiguration = _staticDataService.GetConfiguration<GameHubConfiguration>();

            LevelView levelView = await _prefabFactory.CreateAsync(_gameHubConfiguration.LevelViewPrebafReference);
            var presenter = new LevelPresenter(levelView, _signalBus);
            _disposableObjects.Add(presenter);

            Action destroyCallback = () => OnLevelViewDestroy(levelView);
            _destroyCallbacks.Add(levelView, destroyCallback);
            levelView.Destroyed += destroyCallback;

            _localizedTermProcessorLinker.Link(levelConfiguration.Title, levelView.SetTitle);
            levelView.Set(levelConfiguration.LevelCode);

            return levelView;
        }

        private void OnLevelViewDestroy(LevelView levelView)
        {
            if (_destroyCallbacks.TryPopValue(levelView, out Action destroyCallback))
                levelView.Destroyed -= destroyCallback;
            
            _localizedTermProcessorLinker.Unlink(levelView.SetTitle);
        }
    }
}