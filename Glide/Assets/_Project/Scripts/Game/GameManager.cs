using UnityEngine;

namespace Gisha.Glide.Game
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
            if (Input.GetKeyDown(KeyCode.Escape))
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
            var data = SaveSystem.LoadLevelsData();
            data.allLevels[CoordsManager.CurrentCoords].SetLevelState(LevelState.Passed);

            var nextCoords = CoordsManager.GetNextCoords();
            if (data.allLevels[nextCoords].LevelState == LevelState.Nonexistent)
            {
                Debug.LogError($"Level at coords {nextCoords.DebugText} is Nonexistent!");
                return;
            }

                data.allLevels[nextCoords].SetLevelState(LevelState.Next);
                SaveSystem.SaveLevelsData(data);

                SceneLoader.LoadNextLevel();
        }
    }
}