using Gisha.Glide.Game;
using System;
using UnityEngine;

namespace Gisha.Glide.AirplaneGeneric
{
    [RequireComponent(typeof(AirplaneMovement))]
    public class Airplane : MonoBehaviour
    {
        [Header("Values")]
        [SerializeField] private float defaultWasteOfEnergyInSeconds = 15f;
        [SerializeField] private float boostedWasteOfEnergyInSeconds = 10f;

        [Header("Visual")]
        [SerializeField] private TrailRenderer[] engineTrails = default;

        public event Action<bool> OnCharge;

        public bool IsBoostedSpeed { get; private set; } = false;

        public float Energy
        {
            get => _energy;
            set => _energy = Mathf.Clamp01(value);
        }
        float _energy = 1f;

        public bool InEnoughEnergy => Energy > 0;

        private void Awake()
        {
            OnCharge += ActivateTrails;
        }

        private void Start()
        {
            ChargeUp();
        }

        private void OnDisable()
        {
            OnCharge -= ActivateTrails;
        }

        private void Update()
        {
            if (!InEnoughEnergy)
                return;

            if (Input.GetKey(KeyCode.LeftControl))
                IsBoostedSpeed = true;
            else
                IsBoostedSpeed = false;

            var wasteOfEnergy = IsBoostedSpeed ? boostedWasteOfEnergyInSeconds : defaultWasteOfEnergyInSeconds;
            Energy -= Time.deltaTime / (wasteOfEnergy + 0.001f);

            if (!InEnoughEnergy)
                OnCharge(false);
        }

        public void Die()
        {
            Debug.Log("<color=purple>Airplane was destroyed!</color>");
            GameManager.ReloadLevel();
        }

        public void ChargeUp()
        {
            Energy = 1f;
            OnCharge(true);
        }

        private void ActivateTrails(bool status)
        {
            foreach (var trail in engineTrails)
                trail.enabled = status;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Obstacle"))
                Die();
        }
    }
}