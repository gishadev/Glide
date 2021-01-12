using UnityEngine;

namespace Gisha.Glide.Game.AirplaneGeneric.HUD
{
    public class HUDManager : MonoBehaviour
    {
        #region Singleton
        public static HUDManager Instance { get; private set; }

        [Header("Components")]
        [SerializeField] private ModulesHUD modulesHUD = default;
        public ModulesHUD ModulesHUD => modulesHUD;
        #endregion

        private void Awake()
        {
            Instance = this;

            if (modulesHUD == null)
                Debug.LogError("modulesHUD is not assigned");
        }
    }
}