using UnityEngine;

namespace Gisha.Glide.AirplaneGeneric
{
    public class Airplane : MonoBehaviour
    {
        [SerializeField] private float fullWasteOfEnergyInSeconds = default;

        public float Energy
        {
            get => _energy;
            set => _energy = Mathf.Clamp01(value);
        }
        float _energy = 1f;

        public bool IsCharged => Energy > 0;

        private void Update()
        {
            if (IsCharged)
                Energy -= Time.deltaTime / (fullWasteOfEnergyInSeconds + 0.001f);
        }

        public void ChargeUp() => Energy = 1f;
    }
}