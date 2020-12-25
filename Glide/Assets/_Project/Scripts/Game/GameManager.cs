using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

namespace Gisha.Glide.Game
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance { get; private set; }
        #endregion

        public const string relativePath = "_Project/Scenes";

        public string gameScene;
        public string[] levelScenes;

        private void Awake()
        {
            CreateInstance();

            if (gameScene == null) Debug.LogError("You haven't selected gameScene!");
            if (levelScenes == null || levelScenes.Length == 0) Debug.LogError("You haven't selected any levelScene!");
        }

        private void Start()
        {
            ReloadScene();
        }

        private void CreateInstance()
        {
            DontDestroyOnLoad(gameObject);

            if (Instance == null)
                Instance = this;

            if (Instance != null && Instance != this)
                Destroy(gameObject);
        }

        private static string GetPathToScene(string sceneName) => $"{relativePath}/{sceneName}";

        private static void LoadLevel(int index)
        {
            SceneManager.LoadScene(GetPathToScene(Instance.gameScene));
            SceneManager.LoadScene(GetPathToScene(Instance.levelScenes[index]), LoadSceneMode.Additive);
        }

        public static void ReloadScene() => LoadLevel(0);
    }
}