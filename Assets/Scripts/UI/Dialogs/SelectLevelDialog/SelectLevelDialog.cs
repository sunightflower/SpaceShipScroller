using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


namespace UI.Dialogs
{
    public class SelectLevelDialog : Dialog
    {
        [SerializeField] private GridLayoutGroup _levelsGrid;
        [SerializeField] private SelectLevelSlot _levelSlotPrefab;
        private ILevelLoader _levelLoader;

        private void Start()
        {
            _levelLoader = ServiceLocator.Current.Get<ILevelLoader>();
            var levels = _levelLoader.GetLevels();
            levels = levels.OrderBy(x => x.ID);
            GenerateLevels(levels);
        }

        private void GenerateLevels(IEnumerable<LevelData> levels)
        {
            foreach (var level in levels)
            {
                var go = GameObject.Instantiate(_levelSlotPrefab, _levelsGrid.transform);
                go.Init(level);
            }
        }
    }
}
