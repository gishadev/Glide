using Gisha.Glide.Game;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Gisha.Glide.MainMenu.Levels
{
    public class LevelsChoosingMenu : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private LevelsData levelsData = default;

        [Header("UI")]
        [SerializeField] private Transform galaxyTrans = default;
        [SerializeField] private WorldUI[] worldsUI = default;

        [Header("Colors")]
        [SerializeField] private Color passedColor = default;
        [SerializeField] private Color nextColor = default;
        [SerializeField] private Color nonexistentColor = default;

        public Transform WorldsParent => galaxyTrans.GetChild(0);

        private void OnValidate()
        {
            for (int i = 0; i < worldsUI.Length; i++)
                WorldsParent.GetChild(i).name = worldsUI[i].name;
        }

        private void Start()
        {
            UpdateUIFromData();
        }

        [ContextMenu("Update UI From Data")]
        private void UpdateUIFromData()
        {
            for (int i = 0; i < levelsData.galaxies.Length; i++)
                for (int j = 0; j < levelsData.galaxies[i].worlds.Length; j++)
                    for (int p = 0; p < levelsData.galaxies[i].worlds[j].levels.Length; p++)
                    {
                        var level = levelsData.galaxies[i].worlds[j].levels[p];
                        var levelUI = worldsUI[j].levelsUI[p];
                        var state = level.levelState;
                        switch (state)
                        {
                            case LevelState.Passed:
                                levelUI.ChangeColor(passedColor);
                                break;
                            case LevelState.Next:
                                levelUI.ChangeColor(nextColor);
                                break;
                            case LevelState.Nonexistent:
                                levelUI.gameObject.SetActive(false);
                                levelUI.ChangeColor(nonexistentColor);
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

            var allCoords = new List<LevelCoords>();

            var galaxies = new GalaxyData[1];
            galaxies[0].galaxyName = galaxyTrans.name;

            var worlds = new WorldData[worldsUI.Length];

            for (int i = 0; i < worlds.Length; i++)
            {
                worlds[i] = new WorldData();
                worlds[i].worldName = worldsUI[i].name;

                worlds[i].levels = new LevelData[worldsUI[i].levelsUI.Length];

                for (int j = 0; j < worlds[i].levels.Length; j++)
                {
                    //////////
                    // COORDS
                    //////////
                    var coords = new LevelCoords(0, i, j);
                    allCoords.Add(coords);

                    ///////////////
                    // LEVELS DATA
                    ///////////////
                    string pathToLevelSceneAsset = $"Assets/{PathBuilder.GetSceneAssetPathFromNames(galaxies[0].galaxyName, worlds[i].worldName, j)}.unity";
                    if (!File.Exists(pathToLevelSceneAsset))
                    {
                        Debug.Log($"<color=red>Nonexistent scene asset:</color>{pathToLevelSceneAsset}");
                        worlds[i].levels[j].levelState = LevelState.Nonexistent;
                    }
                    else
                        worlds[i].levels[j].levelState = i == 0 && j == 0 ? LevelState.Next : LevelState.Hidden;

                    worlds[i].levels[j].levelScene = (SceneAsset)AssetDatabase.LoadAssetAtPath(pathToLevelSceneAsset, typeof(SceneAsset));
                }
            }

            galaxies[0].worlds = worlds;

            SceneLoader.SetNewLevelsData(allCoords, galaxies);
        }

        private void UpdateUIFromScene()
        {
            worldsUI = new WorldUI[WorldsParent.childCount];

            // Worlds UI //
            for (int i = 0; i < worldsUI.Length; i++)
            {
                var worldTrans = WorldsParent.GetChild(i);

                var levelsUI = worldTrans.GetComponentsInChildren<LevelUI>(true);
                worldsUI[i] = new WorldUI(worldTrans.name, levelsUI);

                // Levels UI //
                for (int j = 0; j < worldsUI[i].levelsUI.Length; j++)
                {
                    var coords = new LevelCoords(0, i, j);

                    LevelUI level = worldsUI[i].levelsUI[j];
                    level.Coords = coords;
                }
            }
        }
        #endregion
    }

    [System.Serializable]
    public class WorldUI
    {
        public string name;
        public LevelUI[] levelsUI;

        public WorldUI(string name, LevelUI[] levelsUI)
        {
            this.name = name;
            this.levelsUI = levelsUI;
        }
    }
}