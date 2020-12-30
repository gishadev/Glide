using UnityEngine;

namespace Gisha.Glide.Level
{
    public class LevelManager : MonoBehaviour
    {
        #region Singleton
        public static LevelManager Instance { get; private set; }
        #endregion

        [Header("General")]
        [SerializeField] private LevelSettings settings = default;

        [Header("Links")]
        [SerializeField] private Transform spawnpoint = default;
        [SerializeField] private Transform tunnelParent = default;

        public Transform Spawnpoint => spawnpoint;

        private void Awake()
        {
            Instance = this;

            if (spawnpoint == null)
                Debug.LogError("Spawnpoint is not assigned!");
            if (tunnelParent == null)
                Debug.LogError("Tunnel Parent is not assigned!");
        }

        private void OnValidate()
        {
            if (spawnpoint == null || tunnelParent == null)
                return;

            foreach (Renderer renderer in tunnelParent.GetComponentsInChildren<Renderer>())
                renderer.material = settings.TunnelMaterial;
        }
    }
}