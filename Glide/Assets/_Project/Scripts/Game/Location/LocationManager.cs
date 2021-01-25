using Gisha.Glide.Game.AirplaneGeneric;
using Gisha.Glide.Game.HUD;
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

        [Header("Fog")]
        [SerializeField] private Color fogColor = default;
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
            CanvasFader.FadeOut();
            AirplaneSpawner.Instance.SpawnAirplane(Spawnpoint.position);
            Cursor.lockState = CursorLockMode.Locked;

            UpdateFogColor();
        }

        private void OnValidate()
        {
            if (spawnpoint == null || boundsParent == null)
                return;

            foreach (Renderer renderer in boundsParent.GetComponentsInChildren<Renderer>())
                renderer.material = tunnelMaterial;

            UpdateFogColor();
        }

        private void UpdateFogColor() => RenderSettings.fogColor = fogColor;
    }
}