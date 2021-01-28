using UnityEngine.SceneManagement;

namespace Gisha.Glide.Game.Core.SceneLoading
{
    public static class SceneLoader
    {
        public static string CurrentSceneName => SceneManager.GetActiveScene().name;

        public static void LoadLevel(LevelCoords coords)
        {
            CoordsManager.SetCoords(coords);

            var mainPath = PathBuilder.GetPathToMainScene("Game");
            var levelPath = PathBuilder.GetScenePathFromCoords(coords);

            LoadingManager.AsyncLoad(mainPath, levelPath);
        }

        public static void LoadMainMenu()
        {
            CoordsManager.SetCoords(new LevelCoords(-1, -1, -1));

            var mainPath = PathBuilder.GetPathToMainScene("MainMenu");
            LoadingManager.AsyncLoad(mainPath);
        }

        public static void LoadNextLevel() => LoadLevel(CoordsManager.MoveNext());

        public static void ReloadLevel() => LoadLevel(CoordsManager.CurrentCoords);
    }
}