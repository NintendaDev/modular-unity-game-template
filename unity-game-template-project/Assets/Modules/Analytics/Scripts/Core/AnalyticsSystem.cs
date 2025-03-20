using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using Modules.Analytics.Configurations;
using Modules.Analytics.Types;
using Modules.AssetsManagement.StaticData;
using Modules.Logging;
using Modules.Advertisements.Types;
using Modules.Text;
using Modules.Wallet.Types;

namespace Modules.Analytics
{
    public abstract class AnalyticsSystem : IAnalyticsSystem
    {
        private readonly StringBuilder _builder = new();
        private readonly IStaticDataService _staticDataService;
        private readonly HashSet<AnalyticsEventCode> _sendedEventsCodes = new();
        private CustomAnalyticsEventsHub _hub;

        public AnalyticsSystem(ILogSystem logSystem, IStaticDataService staticDataService)
        {
            LogSystem = new TemplateLogger(logSystem);
            _staticDataService = staticDataService;
        }

        protected TemplateLogger LogSystem { get; }

        protected DefaultAnalyticsParamsNames DefaultParamsNames { get; private set; }

        public virtual UniTask InitializeAsync()
        {
            _hub = _staticDataService.GetConfiguration<CustomAnalyticsEventsHub>();

            DefaultParamsNames = _staticDataService.GetConfiguration<DefaultAnalyticsParamsNames>();

            return UniTask.CompletedTask;
        }

        public bool CanSendEvent(AnalyticsEventCode eventCode)
        {
            if (_sendedEventsCodes.Contains(eventCode) == false)
                return true;

            if (_hub.IsSentOnce(eventCode) == false)
                return true;

            return false;
        }

        public abstract void SendCustomEvent(string eventName);
        
        public abstract void SendCustomEvent(string eventName, Dictionary<string, object> data);
        
        public abstract void SendCustomEvent(string eventName, float value);

        public void SendCustomEvent(AnalyticsEventCode eventCode)
        {
            if (CanSendEvent(eventCode) == false)
                return;
                
            _sendedEventsCodes.Add(eventCode);
            SendCustomEventInternal(eventCode);
        }

        public void SendCustomEvent(AnalyticsEventCode eventCode, Dictionary<string, object> data)
        {
            if (CanSendEvent(eventCode) == false)
                return;
                
            _sendedEventsCodes.Add(eventCode);
            SendCustomEventInternal(eventCode, data);
        }

        public void SendCustomEvent(AnalyticsEventCode eventCode, float value)
        {
            if (CanSendEvent(eventCode) == false)
                return;
                
            _sendedEventsCodes.Add(eventCode);
            SendCustomEventInternal(eventCode, value);
        }

        public abstract void SendInterstitialEvent(AdvertisementAction advertisementAction,
            AdvertisementPlacement placement, AdvertisementsSystemType systemType);

        public abstract void SendRewardEvent(AdvertisementAction advertisementAction, AdvertisementPlacement placement,
            AdvertisementsSystemType systemType);
        
        public abstract void SendErrorEvent(LogLevel logLevel, string message);

        public abstract void SendProgressEvent(ProgressStatus progressStatus, string levelName, int progressPercent);

        public abstract void SendProgressEvent(ProgressStatus progressStatus, string levelType, string levelName,
            int progressPercent);
        
        protected abstract void SendCustomEventInternal(AnalyticsEventCode eventCode);
        
        protected abstract void SendCustomEventInternal(AnalyticsEventCode eventCode, Dictionary<string, object> data);
        
        protected abstract void SendCustomEventInternal(AnalyticsEventCode eventCode, float value);

        protected bool IsExistEvent(AnalyticsEventCode eventCode, AnalyticsSystemCode analyticsSystemCode,
            out string eventName)
        {
            bool isExist = _hub.IsExistEventName(eventCode, analyticsSystemCode, out eventName);

            if (isExist == false)
            {
                LogSystem.LogWarning($"The metric name for analyticsSystemCode={analyticsSystemCode} " +
                                     $"and eventCode={eventCode} was not found.");
            }

            return isExist;
        }

        protected string ConvertToSnakeCase(string parameterName) => parameterName.ToSnakeCase();

        protected void LogEvent(AnalyticsEventCode eventCode) => LogEvent(eventCode.ToString());
        
        protected void LogEvent(string eventName)
        {
            _builder.Clear();
            
            _builder.Append($"Custom event have been sent: ");
            _builder.Append($"eventCode={eventName}");
            LogSystem.Log(_builder.ToString());
        }
        
        protected void LogEvent(string eventName, float value)
        {
            _builder.Clear();
            
            _builder.Append($"Custom event have been sent: ");
            _builder.Append($"eventCode={eventName}, ");
            _builder.Append($"value={value}");
            LogSystem.Log(_builder.ToString());
        }
        
        protected void LogEvent(string eventName, Dictionary<string, object> data)
        {
            _builder.Clear();
            
            _builder.Append($"Custom event have been sent: ");
            _builder.Append($"eventCode={eventName}. Parameters: ");
            
            foreach (KeyValuePair<string, object> pair in data)
                _builder.Append($"{pair.Key}={pair.Value}, ");
            
            LogSystem.Log(_builder.ToString().Trim(','));
        }
        
        protected void LogEvent(LogLevel logLevel, string message)
        {
            _builder.Clear();
            
            _builder.Append($"Error event have been sent: ");
            _builder.Append($"logLevel={logLevel}, ");
            _builder.Append($"message={message}");
            LogSystem.Log(_builder.ToString());
        }
        
        protected void LogEvent(AdvertisementAction advertisementAction, AdvertisementPlacement placement,
            AdvertisementsSystemType systemType)
        {
            _builder.Clear();

            _builder.Append("Advertisement statistics have been sent: ");
            _builder.Append($"{nameof(advertisementAction)} = {advertisementAction.ToString().ToSnakeCase()}, ");
            _builder.Append($"{nameof(systemType)}={systemType.ToString().ToSnakeCase()}, ");
            _builder.Append($"{nameof(placement)}={placement.ToString().ToSnakeCase()}");
            LogSystem.Log(_builder.ToString());
        }
        
        protected void LogEvent(ProgressStatus progressStatus, string levelType, string levelName, 
            int progressPercent)
        {
            _builder.Clear();

            _builder.Append("Progress statistics have been sent: ");
            _builder.Append($"{nameof(progressStatus)} = {progressStatus.ToString().ToSnakeCase()}, ");
            _builder.Append($"{nameof(levelType)}={levelType}, ");
            _builder.Append($"{nameof(levelName)}={levelName}, ");
            _builder.Append($"{nameof(progressPercent)}={progressPercent}");
            LogSystem.Log(_builder.ToString());
        }
        
        protected void LogEvent(ResourceFlowType flowType, CurrencyType currencyType, float amount, string itemType, 
            string itemId)
        {
            _builder.Clear();

            _builder.Append("Resource statistics have been sent: ");
            _builder.Append($"{nameof(flowType)} = {flowType.ToString().ToSnakeCase()}, ");
            _builder.Append($"{nameof(currencyType)}={currencyType.ToString().ToSnakeCase()}, ");
            _builder.Append($"{nameof(amount)}={amount}, ");
            _builder.Append($"{nameof(itemType)}={itemType}, ");
            _builder.Append($"{nameof(itemId)}={itemId}");
            LogSystem.Log(_builder.ToString());
        }
        
        protected void LogEvent(AdvertisementRevenue revenue)
        {
            _builder.Clear();

            _builder.Append("Advertisement Revenue event have been sent: ");
            _builder.Append($"{nameof(revenue.Revenue)} = {revenue.Revenue}, ");
            _builder.Append($"{nameof(revenue.Currency)}={revenue.Currency}, ");
            _builder.Append($"{nameof(revenue.AdvertisementsSystemName)}={revenue.AdvertisementsSystemName}, ");
            _builder.Append($"{nameof(revenue.AdvertisementUnitName)}={revenue.AdvertisementUnitName}, ");
            _builder.Append($"{nameof(revenue.Format)}={revenue.Format}, ");
            _builder.Append($"{nameof(revenue.NetworkName)}={revenue.NetworkName}");
            LogSystem.Log(_builder.ToString());
        }
    }
}
