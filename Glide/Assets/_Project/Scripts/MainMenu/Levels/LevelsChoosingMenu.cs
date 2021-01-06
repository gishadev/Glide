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

        public Transform WorldsParent => galaxyTrans.GetChild(0);

        //private void OnValidate()
        //{
        //    for (int i = 0; i < worldsUI.Length; i++)
        //        worldsParent.GetChild(i).name = worldsUI[i].name;
        //}

        [ContextMenu("Set Data From UI")]
        private void UpdateDataFromUI()
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

                worlds[i].levelScenes = new SceneAsset[worldsUI[i].levelsUI.Length];

                for (int j = 0; j < worlds[i].levelScenes.Length; j++)
                {
                    var coords = new LevelCoords(0, i, j);
                    allCoords.Add(coords);

                    string pathToLevelSceneAsset = $"Assets/{PathBuilder.GetSceneAssetPathFromNames(galaxies[0].galaxyName, worlds[i].worldName, j)}.unity";
                    if (!File.Exists(pathToLevelSceneAsset))
                        Debug.Log($"<color=red>Nonexistent scene asset:</color>{pathToLevelSceneAsset}");

                    worlds[i].levelScenes[j] = AssetDatabase.LoadAssetAtPath(pathToLevelSceneAsset, typeof(SceneAsset)) as SceneAsset;
                }
            }

            galaxies[0].worlds = worlds;

            SceneLoader.SetLevelsData(allCoords, galaxies);
        }

        [ContextMenu("Update UI From Data")]
        private void UpdateUIFromLevelsData()
        {

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