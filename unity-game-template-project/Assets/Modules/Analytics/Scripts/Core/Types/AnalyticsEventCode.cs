namespace Modules.Analytics.Types
{
    public enum AnalyticsEventCode
    {
        None = 0,
        AdvertisementRequest = 1,
        AdvertisementClick = 2,
        AdvertisementRewardReceived = 3,
        AdvertisementFailedShow = 4,
        AdvertisementShow = 5,
        LevelStart = 6,
        LevelComplete= 7,
        LevelFail = 8,
        GameBootBootstrap = 103,
        GameBootAuth = 100,
        GameBootLoadProgress = 102,
        GameBootGameHubBootstrap = 104,
        GameBootRestartGameplayState = 105,
        GameBootMainMenu = 101,
    }
}