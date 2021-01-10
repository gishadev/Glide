using Gisha.Glide.MainMenu.Levels;
using UnityEngine;

namespace Gisha.Glide.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        #region Singleton
        public static MainMenuManager Instance { get; private set; }
        #endregion

        [Header("Components")]
        [SerializeField] private LevelInfoBox levelInfoBox = default;
        public LevelInfoBox LevelInfoBox => levelInfoBox;

        [Header("Menus")]
        [SerializeField] private GameObject modulesMenu = default;


        private void Awake()
        {
            Instance = this;

            if (levelInfoBox == null) Debug.LogError("levelInfoBox is not assigned!");
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        public void OnClick_OpenModulesMenu()
        {
            modulesMenu.SetActive(true);
        }

        public void OnClick_ReturnToMainMenu()
        {
            modulesMenu.SetActive(false);
        }
    }
}