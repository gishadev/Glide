using Gisha.Glide.Level;
using UnityEngine;

namespace Gisha.Glide.AirplaneGeneric
{
    public class AirplaneSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject airplane = default;

        private void Awake()
        {
            if (airplane == null)
                Debug.LogError("airplaneTransform is not assigned!");
            else
                ActivateAirplane(false);
        }

        private void Start()
        {
            airplane.transform.position = LevelManager.Instance.Spawnpoint.transform.position;
            ActivateAirplane(true);
        }

        private void ActivateAirplane(bool status) => airplane.SetActive(status);
    }
}