using Gisha.Glide.Level;
using UnityEngine;

namespace Gisha.Glide.AirplaneGeneric
{
    public class AirplaneSpawner : MonoBehaviour
    {
        #region Singleton
        public static AirplaneSpawner Instance { get; private set; }
        #endregion

        [SerializeField] private Airplane airplane = default;

        public Airplane Airplane => airplane;

        private void Awake()
        {
            Instance = this;

            if (airplane == null)
                Debug.LogError("airplane is not assigned!");
            else
                ActivateAirplane(false);
        }

        private void Start()
        {
            airplane.transform.position = LevelManager.Instance.Spawnpoint.transform.position;
            ActivateAirplane(true);
        }

        private void ActivateAirplane(bool status) => airplane.gameObject.SetActive(status);
    }
}