using Gisha.Glide.Game;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

namespace Gisha.Glide.MainMenu.Levels
{
    public class LevelsChoosingMenu : MonoBehaviour
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
            UpdateUIFromScene();
            UpdateUIFromData();
        }

        private void UpdateUIFromData()
        {
            LevelsData data = SaveSystem.LoadLevelsData();
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
        [ContextMenu("Update Data From UI")]
        private void UpdateLevelsDataFromUI()
        {
            UpdateUIFromScene();

            var allLevels = new Dictionary<LevelCoords, LevelData>();
            var galaxies = new GalaxyData[1];
            galaxies[0].galaxyName = galaxyTrans.name;

            var worldNames = new string[WorldsParent.childCount];

            for (int i = 0; i < worldNames.Length; i++)
            {
                var worldTrans = WorldsParent.GetChild(i);

                worldNames[i] = worldTrans.name;

                for (int j = 0; j < worldTrans.childCount; j++)
                {
                    var pathToLevelSceneAsset = $"Assets/{PathBuilder.GetSceneAssetPathFromNames(galaxies[0].galaxyName, worldNames[i], j)}.unity";

                    LevelState levelState = LevelState.Nonexistent;
                    if (!File.Exists(pathToLevelSceneAsset))
                        Debug.Log($"<color=red>Nonexistent scene asset:</color>{pathToLevelSceneAsset}");
                    else
                        levelState = i == 0 && j == 0 ? LevelState.Next : LevelState.Hidden;

                    allLevels.Add(new LevelCoords(0, i, j), new LevelData((int)levelState));
                }
            }

            galaxies[0].worldNames = worldNames;

            levelsMap.galaxies = galaxies;
            SaveSystem.SaveLevelsData(new LevelsData(allLevels));

            UpdateUIFromData();
        }

        private void UpdateUIFromScene()
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