using System;
using Game.Application.Common;
using Modules.Core.Systems;
using Modules.Core.UI;

namespace Game.Application.GameHub.UI
{
    public sealed class LevelView : UITextButton, IDestroyEvent
    {
        public event Action Destroyed;
        
        public LevelCode LevelCode { get; private set; }

        public void Set(LevelCode levelCode) =>
            LevelCode = levelCode;
    }
}
