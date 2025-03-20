using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Application.Common
{
    [CreateAssetMenu(fileName = "new LevelsConfigurationsHub", menuName = "GameTemplate/Levels/LevelsConfigurationsHub")]
    public sealed class LevelsConfigurationsHub : ScriptableObject
    {
        [ValidateInput(nameof(IsUniqueLevel))]
        [SerializeField] private LevelConfiguration[] _levelsConfigurations;

        public IEnumerable<LevelConfiguration> LevelsConfigurations => _levelsConfigurations;

        public bool TryGetLevelConfiguration(LevelCode levelCode, out LevelConfiguration levelConfiguration)
        {
            levelConfiguration = _levelsConfigurations.Where(x => x.LevelCode == levelCode).FirstOrDefault();

            return levelConfiguration != null;
        }
        private bool IsUniqueLevel(LevelConfiguration[] levelsConfigurations, ref string errorMessage)
        {
            errorMessage = "Levels Code is not unique";

            return levelsConfigurations.GroupBy(x => x.LevelCode).Count() == levelsConfigurations.Length;
        }
    }
}
