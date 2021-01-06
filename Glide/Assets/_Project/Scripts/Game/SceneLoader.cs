using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace Gisha.Glide.Game
{
    public static class SceneLoader
    {
        public static LevelCoords currentCoords;

        public static void SetLevelsData(List<LevelCoords> coords, GalaxyData[] galaxies)
        {
            var data = AssetDatabase.LoadAssetAtPath(PathBuilder.LevelsDataRelativePath, typeof(LevelsData)) as LevelsData;
            data.levelsCoords = coords;
            data.galaxies = galaxies;
        }

        public static void LoadLevel(LevelCoords coords)
        {
            currentCoords = coords;

            SceneManager.LoadScene(PathBuilder.GetPathToMainScene("Game"));
            SceneManager.LoadScene(PathBuilder.GetSceneAssetPathFromCoords(currentCoords), LoadSceneMode.Additive);
        }

        public static void LoadMainMenu()
        {
            currentCoords = new LevelCoords(-1, -1, -1);
            SceneManager.LoadScene(PathBuilder.GetPathToMainScene("MainMenu"), LoadSceneMode.Single);
        }

        public static void LoadNextLevel()
        {
            currentCoords = new LevelCoords(currentCoords.GalaxyID, currentCoords.WorldID, currentCoords.LevelID + 1);
            LoadLevel(currentCoords);
        }

        public static void LoadPreviousLevel()
        {
            currentCoords = new LevelCoords(currentCoords.GalaxyID, currentCoords.WorldID, currentCoords.LevelID - 1);
            LoadLevel(currentCoords);
        }

        public static void ReloadLevel() => LoadLevel(currentCoords);
    }

    public struct LevelCoords
    {
        public int GalaxyID { private set; get; }
        public int WorldID { private set; get; }
        public int LevelID { private set; get; }

        public LevelCoords(int galaxyID, int worldID, int levelID)
        {
            this.GalaxyID = galaxyID;
            this.WorldID = worldID;
            this.LevelID = levelID;
        }
    }

    public static class PathBuilder
    {
        // PATHES //
        public const string MainRelativePath = "_Project/Scenes/Main";
        public const string GalaxiesRelativePath = "_Project/Scenes/Galaxies";

        public const string LevelsDataRelativePath = "Assets/_Project/ScriptableObjects/LevelsData.asset";

        public static LevelsData LevelsDataAsset
        => AssetDatabase.LoadAssetAtPath(LevelsDataRelativePath, typeof(LevelsData)) as LevelsData;

        public static string GetSceneAssetPathFromCoords(LevelCoords coords)
        {
            var data = LevelsDataAsset;

            return $"{GalaxiesRelativePath}/" +
                $"{data.galaxies[coords.GalaxyID].galaxyName}/" +
                $"{data.galaxies[coords.GalaxyID].worlds[coords.WorldID].worldName}/" +
                $"Level {coords.LevelID + 1}";
        }

        public static string GetSceneAssetPathFromNames(string galaxyName, string worldName, int levelIndex)
        {
            return $"{GalaxiesRelativePath}/{galaxyName}/{worldName}/Level {levelIndex + 1}";
        }

        public static string GetPathToMainScene(string sceneName) 
            => $"{MainRelativePath}/{sceneName}";
    }
}