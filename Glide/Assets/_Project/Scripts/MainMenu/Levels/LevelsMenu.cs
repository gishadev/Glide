using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Gisha.Glide.Game.Core;

namespace Gisha.Glide.MainMenu.Levels
{
    public class LevelsMenu : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private LevelsMap levelsMap = default;
        [SerializeField] private Transform galaxyTrans = default;

        [Header("Colors")]
        [SerializeField] private Color passedColor = default;
        [SerializeField] private Color nextColor = default;
        [SerializeField] private Color nonexistentColor = default;
        public Transform WorldsParent => galaxyTrans.GetChild(0);

        Dictionary<LevelCoords, LevelUI> _levelsUI = new Dictionary<LevelCoords, LevelUI>();

        private void Start()
        {
            UpdateUI();
        }

        [ContextMenu("Update UI")]
        public void UpdateUI()
        {
            LevelsData data = SaveSystem.LoadLevelsData();
            CreateLevelsUIFromScene();

            if (data.allLevels == null || data.allLevels.Count == 0)
            {
                ResetLevelsData();
                return;
            }

            var keys = new List<LevelCoords>(_levelsUI.Keys);
            for (int i = 0; i < keys.Count; i++)
            {
                var key = keys[i];
                var levelUI = _levelsUI[key];
                var state = data.allLevels[key].LevelState;
                switch (state)
                {
                    case LevelState.Passed:
                        levelUI.ChangeColor(passedColor);
                        levelUI.gameObject.SetActive(true);
                        break;
                    case LevelState.Next:
                        levelUI.ChangeColor(nextColor);
                        levelUI.gameObject.SetActive(true);
                        break;
                    case LevelState.Nonexistent:
                        levelUI.ChangeColor(nonexistentColor);
                        levelUI.gameObject.SetActive(false);
                        break;
                    case LevelState.Hidden:
                        levelUI.gameObject.SetActive(false);
                        break;
                }
            }
        }

        #region Data Updater
        [ContextMenu("Reset Data")]
        public void ResetLevelsData()
        {
            CreateLevelsUIFromScene();
            LevelsManager.UpdateLevelsMap(galaxyTrans, levelsMap);
            LevelsManager.CreateLevelsData(levelsMap);
            UpdateUI();
        }

        private void CreateLevelsUIFromScene()
        {
            _levelsUI = new Dictionary<LevelCoords, LevelUI>();

            for (int i = 0; i < WorldsParent.childCount; i++)
            {
                var worldTrans = WorldsParent.GetChild(i);

                var childrenLevelsUI = worldTrans.GetComponentsInChildren<LevelUI>(true);
                for (int j = 0; j < childrenLevelsUI.Length; j++)
                {
                    var coords = new LevelCoords(0, i, j);

                    childrenLevelsUI[j].Coords = coords;
                    _levelsUI.Add(coords, childrenLevelsUI[j]);
                }
            }
        }
        #endregion
    }
}