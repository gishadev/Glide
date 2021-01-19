using Gisha.Glide.Game.AirplaneGeneric;
using UnityEngine;

namespace Gisha.Glide.Game.Location
{
    public class LocationManager : MonoBehaviour
    {
        #region Singleton
        public static LocationManager Instance { get; private set; }
        #endregion

        [Header("General")]
        [SerializeField] private Transform spawnpoint = default;
        [SerializeField] private Transform boundsParent = default;

        [Header("Bounds")]
        [SerializeField] private Material tunnelMaterial = default;

        public Transform Spawnpoint => spawnpoint;

        private void Awake()
        {
            Instance = this;

            if (spawnpoint == null)
                Debug.LogError("spawnpoint is not assigned");
            if (boundsParent == null)
                Debug.LogError("boundsParent is not assigned");
        }

        private void Start()
        {
            AirplaneSpawner.Instance.SpawnAirplane(Spawnpoint.position);
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnValidate()
        {
            if (spawnpoint == null || boundsParent == null)
                return;

            foreach (Renderer renderer in boundsParent.GetComponentsInChildren<Renderer>())
                renderer.material = tunnelMaterial;
        }
    }
}