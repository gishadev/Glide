using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine;

namespace Gisha.Glide.Game.Core
{
    public static class SceneLoader
    {
        public static void LoadLevel(LevelCoords coords)
        {
            CoordsManager.SetCoords(coords);

            SceneManager.LoadScene(PathBuilder.GetPathToMainScene("Game"));
            SceneManager.LoadScene(PathBuilder.GetScenePathFromCoords(CoordsManager.CurrentCoords), LoadSceneMode.Additive);

            var data = SaveSystem.LoadLevelsData();

            Debug.Log($"<color=green>Level at {coords.DebugText} was loaded! Best score: {data.allLevels[coords].BestScore}</color>");
        }

        public static void LoadMainMenu()
        {
            CoordsManager.SetCoords(new LevelCoords(-1, -1, -1));
            SceneManager.LoadScene(PathBuilder.GetPathToMainScene("MainMenu"), LoadSceneMode.Single);
        }

        public static void LoadNextLevel() => LoadLevel(CoordsManager.MoveNext());

        public static void ReloadLevel() => LoadLevel(CoordsManager.CurrentCoords);
    }

    public static class CoordsManager
    {
        public static LevelCoords CurrentCoords { get; private set; }

        public static void SetCoords(LevelCoords coords) => CurrentCoords = coords;

        public static LevelCoords GetNextCoords()
        {
            var data = SaveSystem.LoadLevelsData();

            var maxLevelsInWorld = data.allLevels
                .Where(x => x.Key.WorldID == CurrentCoords.WorldID)
                .Max(x => x.Key.LevelID);
            var maxWorldsInGalaxy = data.allLevels
                .Where(x => x.Key.GalaxyID == CurrentCoords.GalaxyID)
                .Max(x => x.Key.WorldID);

            var newLevelID = CurrentCoords.LevelID + 1;
            var newWorldID = CurrentCoords.WorldID + 1;

            if (newLevelID > maxLevelsInWorld && newWorldID > maxWorldsInGalaxy)
            {
                Debug.Log("<color=blue>Player is on last coords.</color>");
                return CurrentCoords;
            }

            var levelID = newLevelID <= maxLevelsInWorld ? newLevelID : 0;
            var worldID = newLevelID > maxLevelsInWorld && newWorldID <= maxWorldsInGalaxy ? newWorldID : CurrentCoords.WorldID;

            return new LevelCoords(CurrentCoords.GalaxyID, worldID, levelID);
        }

        public static LevelCoords MoveNext()
        {
            CurrentCoords = GetNextCoords();
            return CurrentCoords;
        }
    }
}