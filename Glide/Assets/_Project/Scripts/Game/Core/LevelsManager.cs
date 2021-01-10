using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Gisha.Glide.Game.Core
{
    public static class LevelsManager
    {
        public static LevelsData CreateLevelsData(LevelsMap mapData)
        {
            var levels = new Dictionary<LevelCoords, LevelData>();

            for (int g = 0; g < mapData.galaxies.Length; g++)
                for (int w = 0; w < mapData.galaxies[g].worldNames.Length; w++)
                    for (int l = 0; l < mapData.galaxies[g].levelsCount[w]; l++)
                    {
                        var coords = new LevelCoords(g, w, l);
                        var pathToLevelSceneAsset = $"Assets/{PathBuilder.GetScenePathFromCoords(coords)}.unity";

                        var levelState = LevelState.Nonexistent;
                        if (!File.Exists(pathToLevelSceneAsset))
                            Debug.Log($"<color=red>Nonexistent scene asset:</color>{pathToLevelSceneAsset}");
                        else
                            levelState = w == 0 && l == 0 ? LevelState.Next : LevelState.Hidden;

                        levels.Add(new LevelCoords(g, w, l), new LevelData((int)levelState));
                    }

            var data = new LevelsData(levels);
            SaveSystem.SaveLevelsData(data);
            return data;
        }

        public static void UpdateLevelsMap(Transform galaxyTrans, LevelsMap mapAsset)
        {
            var galaxies = new GalaxyData[1];
            var worldsParent = galaxyTrans.GetChild(0);

            var worldNames = new string[worldsParent.childCount];
            var levelsCount = new int[worldsParent.childCount];

            for (int w = 0; w < worldsParent.childCount; w++)
            {
                var worldTrans = worldsParent.GetChild(w);

                worldNames[w] = worldTrans.name;
                levelsCount[w] = worldTrans.childCount;
            }

            galaxies[0].galaxyName = galaxyTrans.name;
            galaxies[0].worldNames = worldNames;
            galaxies[0].levelsCount = levelsCount;

            mapAsset.galaxies = galaxies;

            EditorUtility.SetDirty(mapAsset);
        }
    }
}