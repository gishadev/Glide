using Gisha.Glide.AirplaneGeneric;
using UnityEngine;

namespace Gisha.Glide.Location
{
    public class LocationManager : MonoBehaviour
    {
        #region Singleton
        public static LocationManager Instance { get; private set; }
        #endregion

        [Header("General")]
        [SerializeField] private Transform spawnpoint = default;
        [SerializeField] private Transform tunnelParent = default;

        [Header("Material")]
        [SerializeField] private Material tunnelMaterial = default;

        public Transform Spawnpoint => spawnpoint;

        private void Awake()
        {
            Instance = this;

            if (spawnpoint == null)
                Debug.LogError("Spawnpoint is not assigned!");
            if (tunnelParent == null)
                Debug.LogError("Tunnel Parent is not assigned!");
        }

        private void Start()
        {
            AirplaneSpawner.Instance.SpawnAirplane(Spawnpoint.position);
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnValidate()
        {
            if (spawnpoint == null || tunnelParent == null)
                return;

            foreach (Renderer renderer in tunnelParent.GetComponentsInChildren<Renderer>())
                renderer.material = tunnelMaterial;
        }
    }
}