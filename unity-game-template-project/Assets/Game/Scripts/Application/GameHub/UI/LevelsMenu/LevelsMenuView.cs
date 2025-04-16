using Sirenix.OdinInspector;
using System.Collections.Generic;
using Modules.Core.UI;
using UnityEngine;

namespace Game.Application.GameHub
{
    public sealed class LevelsMenuView : ViewWithBackButton
    {
        [SerializeField, Required] private LevelsPanelView _levelsPanelView;

        public void Link(IEnumerable<LevelView> levelViews) =>
            _levelsPanelView.Link(levelViews);   
    }
}
