using Modules.AssetsManagement.StaticData;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Application.Bootstrap
{
    [CreateAssetMenu(fileName = "new ProjectInstaller", menuName = "GameTemplate/Installers/ProjectInstaller")]
    public class ProjectInstaller : ScriptableObjectInstaller<ProjectInstaller>
    {
        [SerializeField, Required] private GameLoadingAssetsConfiguration _gameLoadingAssetsConfiguration;
        [SerializeField, Required] private StaticDataServiceConfiguration _staticDataServiceConfiguration;
        [SerializeField] private RepositoryInstaller.RepositoryConfiguration _repositoryConfiguration;
        
        public override void InstallBindings()
        {
            CoreInstaller.Install(Container);
            
            AssetManagementInstaller.Install(Container, _gameLoadingAssetsConfiguration,
                _staticDataServiceConfiguration);
            
            DeviceSystemsInstaller.Install(Container);
            LocalizationSystemInstaller.Install(Container);
            PopupsInstaller.Install(Container);
            AuthInstaller.Install(Container);
            AudioInstaller.Install(Container);
            GameDataInstaller.Install(Container);
            RepositoryInstaller.Install(Container, _repositoryConfiguration);
            SaveLoadInstaller.Install(Container);
            SceneManagementInstaller.Install(Container);
            AnalyticsInstaller.Install(Container);
            AdvertisementsInstaller.Install(Container);
            GameBootstrapInstaller.Install(Container);
            GameStateMachineInstaller.Install(Container);
        }
    }
}