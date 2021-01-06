using UnityEngine;

namespace Gisha.Glide.Game
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance { get; private set; }
        #endregion

        [Header("General")]
        [SerializeField] private LevelsData levelsData = default;

        private void Awake()
        {
            CreateInstance();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                SceneLoader.LoadNextLevel();
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                SceneLoader.LoadPreviousLevel();
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

        public void OnPassLevel()
        {
            levelsData.passedCoords.Add(SceneLoader.currentCoords);

            Debug.Log("<color=green>Airplane was teleported!</color>");
            SceneLoader.LoadNextLevel();
        }
    }
}