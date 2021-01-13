using UnityEngine;

namespace Gisha.Glide.Game.HUD
{
    public class HUDManager : MonoBehaviour
    {
        #region Singleton
        public static HUDManager Instance { get; private set; }
        #endregion

        [Header("Components")]
        [SerializeField] private ModulesHUD modulesHUD = default;
        [SerializeField] private ScoreHUD scoreHUD = default;

        public ModulesHUD ModulesHUD => modulesHUD;
        public ScoreHUD ScoreHUD => scoreHUD;

        private void Awake()
        {
            Instance = this;

            if (modulesHUD == null)
                Debug.LogError("modulesHUD is not assigned");
            if (scoreHUD == null)
                Debug.LogError("scoreHUD is not assigned");
        }
    }
}