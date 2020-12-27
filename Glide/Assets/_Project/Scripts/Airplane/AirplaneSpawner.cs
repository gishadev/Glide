using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.Glide.AirplaneGeneric
{
    public class AirplaneSpawner : MonoBehaviour
    {
        #region Singleton
        public static AirplaneSpawner Instance { get; private set; }
        #endregion

        [SerializeField] private Transform airplaneTransform = default;

        private void Awake()
        {
            Instance = this;

            if (airplaneTransform == null) Debug.LogError("Airplane Transform is missing!");
            else ActivateAirplane(false);
        }

        private void Start()
        {
            SetPositionToSpawnpoint();
            ActivateAirplane(true);
        }

        private void SetPositionToSpawnpoint()
        {
            if (Spawnpoint.Instance == null)
                return;

            airplaneTransform.position = Spawnpoint.Instance.Position;
        }

        private void ActivateAirplane(bool status)
        {
            airplaneTransform.gameObject.SetActive(status);
        }
    }
}