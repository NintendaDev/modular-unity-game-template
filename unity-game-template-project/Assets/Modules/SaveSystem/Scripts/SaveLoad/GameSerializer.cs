using System.Collections.Generic;
using Newtonsoft.Json;

namespace Modules.SaveSystem.SaveLoad
{
    public abstract class GameSerializer<TService, TData> : IGameSerializer
    {
        private readonly TService _service;

        protected GameSerializer(TService service)
        {
            _service = service;
        }

        private string Key => typeof(TData).Name;

        public void Serialize(IDictionary<string, string> saveState)
        {
            TData data = Serialize(_service);
            saveState[Key] = JsonConvert.SerializeObject(data);
        }

        public void Deserialize(IDictionary<string, string> loadState)
        {
            if (loadState.TryGetValue(Key, out string json) == false)
                return;

            TData data = JsonConvert.DeserializeObject<TData>(json);
            Deserialize(_service, data);
        }

        protected abstract TData Serialize(TService service);
        
        protected abstract void Deserialize(TService service, TData data);
    }
}