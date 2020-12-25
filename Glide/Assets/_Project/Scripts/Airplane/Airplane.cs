using Gisha.Glide.Game;
using UnityEngine;

namespace Gisha.Glide.AirplaneGeneric
{
    public class Airplane : MonoBehaviour
    {
        [Header("Values")]
        [SerializeField] private float defaultWasteOfEnergyInSeconds = 15f;
        [SerializeField] private float boostedWasteOfEnergyInSeconds = 10f;

        public bool IsBoostedSpeed { get; private set; } = false;

        public float Energy
        {
            get => _energy;
            set => _energy = Mathf.Clamp01(value);
        }
        float _energy = 1f;

        public bool IsCharged => Energy > 0;

        private void Update()
        {
            if (!IsCharged)
            {
                Die();
                return;
            }

            var wasteOfEnergy = IsBoostedSpeed ? boostedWasteOfEnergyInSeconds : defaultWasteOfEnergyInSeconds;
            Energy -= Time.deltaTime / (wasteOfEnergy + 0.001f);

            if (Input.GetKey(KeyCode.LeftControl))
                IsBoostedSpeed = true;
            else
                IsBoostedSpeed = false;
        }

        public void Die()
        {
            Debug.Log("<color=purple>Airplane was destroyed!</color>");
            GameManager.ReloadScene();
        }

        public void ChargeUp() => Energy = 1f;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Obstacle"))
                Die();
        }
    }
}