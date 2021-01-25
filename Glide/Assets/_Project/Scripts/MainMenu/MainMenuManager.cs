using Gisha.Glide.Game.HUD;
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

        private void Awake()
        {
            Instance = this;

            if (levelInfoBox == null) Debug.LogError("levelInfoBox is not assigned!");
        }

        private void Start()
        {
            CanvasFader.FadeOut();
            Cursor.lockState = CursorLockMode.None;
        }
    }
}