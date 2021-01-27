using UnityEngine;

namespace Gisha.Glide.Game.Core
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance { get; private set; }
        #endregion

        private void Awake()
        {
            CreateInstance();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                OnPassLevel();
            if (Input.GetKeyDown(KeyCode.Escape) && SceneLoader.CurrentSceneName != "MainMenu")
                SceneLoader.LoadMainMenu();
        }

        private void CreateInstance()
        {
            DontDestroyOnLoad(gameObject);

            if (Instance == null)
                Instance = this;


            if (Instance != null && Instance != this)
                Destroy(gameObject);
        }

        public static void OnPassLevel()
        {
            Instance.SaveLevelData();
            SceneLoader.LoadNextLevel();
        }

        private void SaveLevelData()
        {
            var data = SaveSystem.LoadLevelsData();

            var currentLevel = data.allLevels[CoordsManager.CurrentCoords];
            var nextLevel = data.allLevels[CoordsManager.GetNextCoords()];

            currentLevel.SetLevelState(LevelState.Passed);

            switch (nextLevel.LevelState)
            {
                case LevelState.Nonexistent:
                    Debug.LogError($"Level at coords {CoordsManager.GetNextCoords().DebugText} is Nonexistent!");
                    return;
                case LevelState.Hidden:
                    nextLevel.SetLevelState(LevelState.Next);
                    break;
            }

            if (currentLevel.BestScore < ScoreProcessor.Score)
                currentLevel.SetBestScore(ScoreProcessor.Score);

            SaveSystem.SaveLevelsData(data);
        }
    }
}