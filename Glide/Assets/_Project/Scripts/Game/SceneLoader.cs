using UnityEngine.SceneManagement;
using System.Linq;

namespace Gisha.Glide.Game
{
    public static class SceneLoader
    {
        public static void LoadLevel(LevelCoords coords)
        {
            CoordsManager.SetCoords(coords);

            SceneManager.LoadScene(PathBuilder.GetPathToMainScene("Game"));
            SceneManager.LoadScene(PathBuilder.GetSceneAssetPathFromCoords(CoordsManager.CurrentCoords), LoadSceneMode.Additive);
        }

        public static void LoadMainMenu()
        {
            CoordsManager.SetCoords(new LevelCoords(-1, -1, -1));
            SceneManager.LoadScene(PathBuilder.GetPathToMainScene("MainMenu"), LoadSceneMode.Single);
        }

        public static void LoadNextLevel()
        {
            CoordsManager.MoveNext();
            LoadLevel(CoordsManager.CurrentCoords);
        }

        //public static void LoadPreviousLevel()
        //{
        //    CoordsManager.MovePrevious();
        //    LoadLevel(CoordsManager.CurrentCoords);
        //}

        public static void ReloadLevel() => LoadLevel(CoordsManager.CurrentCoords);
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

    public static class CoordsManager
    {
        public static LevelCoords CurrentCoords { get; private set; }

        public static void SetCoords(LevelCoords coords) => CurrentCoords = coords;

        public static LevelCoords GetNextCoords()
        {
            var data = PathBuilder.LevelsDataAsset;

            var maxLevelsInWorld = data.allLevels
                .Where(x => x.Key.WorldID == CurrentCoords.WorldID)
                .Max(x => x.Key.LevelID);
            var maxWorldsInGalaxy = data.allLevels
                .Where(x => x.Key.GalaxyID == CurrentCoords.GalaxyID)
                .Max(x => x.Key.WorldID);

            var newLevelID = CurrentCoords.LevelID + 1;
            var newWorldID = CurrentCoords.WorldID + 1;

            var levelID = newLevelID <= maxLevelsInWorld ? newLevelID : 0;
            var worldID = newLevelID > maxLevelsInWorld && newWorldID <= maxWorldsInGalaxy ? newWorldID : CurrentCoords.WorldID;

            return new LevelCoords(CurrentCoords.GalaxyID, worldID, levelID);
        }

        public static void MoveNext() => CurrentCoords = GetNextCoords();
    }
}