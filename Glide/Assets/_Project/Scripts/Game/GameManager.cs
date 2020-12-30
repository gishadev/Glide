using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using Gisha.Glide.Level;
using Gisha.Glide.AirplaneGeneric;

namespace Gisha.Glide.Game
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance { get; private set; }
        #endregion

        [Header("Pathes")]
        [SerializeField] private string mainRelativePath = "_Project/Scenes";
        [SerializeField] private string levelsRelativePath = "_Project/Scenes/Levels";

        [Header("Scenes Names")]
        [SerializeField] private string gameSceneName = default;
        [SerializeField] private string[] levelScenesNames = default;

        public static int CurrentLevelIndex = 0;

        private void Awake()
        {
            CreateInstance();

            if (gameSceneName == null)
                Debug.LogError("You haven't selected Game Scene!");
            if (levelScenesNames == null || levelScenesNames.Length == 0) 
                Debug.LogError("You haven't selected any Level Scene!");
        }

        private void Start()
        {
            ReloadLevel();
        }

        private void CreateInstance()
        {
            DontDestroyOnLoad(gameObject);

            if (Instance == null)
                Instance = this;

            if (Instance != null && Instance != this)
                Destroy(gameObject);
        }

        private static string GetPathToScene(string relativePath, string sceneName) => $"{relativePath}/{sceneName}";

        private static void LoadLevel(int index)
        {
            SceneManager.LoadScene(
                GetPathToScene(Instance.mainRelativePath, Instance.gameSceneName));
            SceneManager.LoadScene(
                GetPathToScene(Instance.levelsRelativePath, Instance.levelScenesNames[index]), LoadSceneMode.Additive);
        }

        public static void LoadNextLevel()
        {
            if (CurrentLevelIndex + 1 < Instance.levelScenesNames.Length)
                CurrentLevelIndex++;
            else
                CurrentLevelIndex = 0;

            LoadLevel(CurrentLevelIndex);
        }

        public static void ReloadLevel() => LoadLevel(CurrentLevelIndex);
    }
}