using Game.Application.Common;
using Modules.EventBus;

namespace Game.Application.GameHub
{
    public sealed class LevelLoadSignal : IPayloadSignal
    {
        public LevelLoadSignal(LevelCode levelCode) =>
            LevelCode = levelCode;

        public LevelCode LevelCode { get; }
    }
}
