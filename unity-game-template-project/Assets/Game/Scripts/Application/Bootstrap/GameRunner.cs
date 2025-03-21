using Game.Application.Bootstrap;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Bootstrap
{
    public sealed class GameRunner : MonoBehaviour
    {
        private GameBootstrapperFactory _bootstrapperFactory;

        [Inject]
        private void Construct(GameBootstrapperFactory bootstrapperFactory)
        {
            _bootstrapperFactory = bootstrapperFactory;
        }
            
        private async void Start()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper != null) 
                return;

            await _bootstrapperFactory.CreateAsync();
        }
    }
}
