using UnityEngine;
using UnityEngine.SceneManagement;

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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                LoadNextLevel();
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                LoadPreviousLevel();
        }

        private void CreateInstance()
        {
            DontDestroyOnLoad(gameObject);

            if (Instance == null)
                Instance = this;

            if (Instance != null && Instance != this)
                Destroy(gameObject);
        }

        private string GetPathToScene(string relativePath, string sceneName) => $"{relativePath}/{sceneName}";

        private void LoadLevel(int index)
        {
            SceneManager.LoadScene(
                GetPathToScene(Instance.mainRelativePath, Instance.gameSceneName));
            SceneManager.LoadScene(
                GetPathToScene(Instance.levelsRelativePath, Instance.levelScenesNames[index]), LoadSceneMode.Additive);
        }

        //////////////////
        /// STATIC ENTRY:
        //////////////////

        public static void LoadNextLevel()
        {
            if (CurrentLevelIndex < Instance.levelScenesNames.Length - 1)
                CurrentLevelIndex++;
            else
                CurrentLevelIndex = 0;

            Instance.LoadLevel(CurrentLevelIndex);
        }

        public static void LoadPreviousLevel()
        {
            if (CurrentLevelIndex > 0)
                CurrentLevelIndex--;
            else
                CurrentLevelIndex = Instance.levelScenesNames.Length - 1;

            Instance.LoadLevel(CurrentLevelIndex);
        }

        public static void ReloadLevel() => Instance.LoadLevel(CurrentLevelIndex);
    }
}