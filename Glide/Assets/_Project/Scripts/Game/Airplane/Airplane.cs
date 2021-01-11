using Gisha.Glide.Game.AirplaneGeneric.Modules;
using Gisha.Glide.Game.Core;
using System;
using UnityEngine;

namespace Gisha.Glide.Game.AirplaneGeneric
{
    [RequireComponent(typeof(AirplaneMovement))]
    public class Airplane : MonoBehaviour
    {
        [Header("Values")]
        [SerializeField] private float defaultWasteOfEnergyInSeconds = 15f;
        [SerializeField] private float boostedWasteOfEnergyInSeconds = 10f;

        [Header("Visual")]
        [SerializeField] private GameObject[] engineVisualObjects = default;

        public event Action<bool> OnCharge;

        public bool IsBoostedSpeed { get; private set; } = false;

        public float Energy
        {
            get => _energy;
            set => _energy = Mathf.Clamp01(value);
        }
        float _energy = 1f;

        public bool InEnoughEnergy => Energy > 0;

        Module[] _modules = new Module[1];
        public void AddModule(Module module)
        {
            _modules[0] = module;
            Debug.Log("New Module was added!");
        }

        private void OnEnable() => OnCharge += OnChargeAirplane;
        private void OnDisable() => OnCharge -= OnChargeAirplane;

        private void Start()
        {
            ChargeUp();
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
                Discharge();

            if (Input.GetKeyDown(KeyCode.E) && _modules[0] != null)
                _modules[0].Use(this);
        }

        public void Die()
        {
            Debug.Log("<color=purple>Airplane was destroyed!</color>");
            SceneLoader.ReloadLevel();
        }

        public void ChargeUp() => OnCharge(true);
        public void Discharge() => OnCharge(false);

        public void OnChargeAirplane(bool status)
        {
            Energy = status ? 1f : 0f;

            // Activate/Deactivate trails.
            foreach (var obj in engineVisualObjects)
                obj.SetActive(status);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Obstacle"))
                Die();
        }
    }
}