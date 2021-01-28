using Gisha.Glide.Game.AirplaneGeneric.Modules;
using Gisha.Glide.Game.Core.SceneLoading;
using Gisha.Glide.Game.Effects;
using Gisha.Glide.Game.HUD;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gisha.Glide.Game.AirplaneGeneric
{
    [RequireComponent(typeof(AirplaneMovement))]
    public class Airplane : MonoBehaviour
    {
        [Header("General")]
        public AirplaneChargeController chargeController;
        public AirplaneModularSystem modularSystem;

        [Header("Visual")]
        [SerializeField] private GameObject[] engineVisualObjects = default;

        private event Action<bool> BoostModeChanged;

        public bool IsBoostedSpeed { get; private set; } = false;
        public bool EnginePushing { get; set; } = true;

        private void Start()
        {
            chargeController.ChargeUp();
        }

        private void OnEnable()
        {
            chargeController.Charged += OnChargeAirplane;
            BoostModeChanged += OnChangeBoostMode;
        }

        private void OnDisable()
        {
            chargeController.Charged -= OnChargeAirplane;
            BoostModeChanged -= OnChangeBoostMode;
        }

        private void Update()
        {
            if (!chargeController.InEnoughEnergy || !EnginePushing)
                return;

            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (!IsBoostedSpeed)
                    BoostModeChanged(true);
            }
            else
            {
                if (IsBoostedSpeed)
                    BoostModeChanged(false);
            }

            if (Input.GetKeyDown(KeyCode.Space) && modularSystem.ModuleExists)
                modularSystem.UseModule(this, 0);

            chargeController.EnergyWaste(IsBoostedSpeed);
        }

        public void OnChargeAirplane(bool status)
        {
            // Activate/Deactivate trails.
            foreach (var obj in engineVisualObjects)
                obj.SetActive(status);
        }

        private void OnChangeBoostMode(bool isBoosted)
        {
            IsBoostedSpeed = isBoosted;

            if (IsBoostedSpeed)
                CameraEffect.ChangeFOV(CameraEffect.CameraEffectState.Boosted);
            else
                CameraEffect.ChangeFOV(CameraEffect.CameraEffectState.Default);
        }

        public void Die()
        {
            Debug.Log("<color=purple>Airplane was destroyed!</color>");
            SceneLoader.ReloadLevel();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Obstacle"))
                Die();
        }
    }

    [Serializable]
    public class AirplaneChargeController
    {
        [Header("Charge Values")]
        [SerializeField] private float defaultWasteOfEnergyInSeconds = 15f;
        [SerializeField] private float boostedWasteOfEnergyInSeconds = 10f;

        public event Action<bool> Charged;

        public float Energy
        {
            get => _energy;
            set => _energy = Mathf.Clamp01(value);
        }
        float _energy = 1f;

        public bool InEnoughEnergy => Energy > 0;

        public void ChargeUp()
        {
            Energy = 1f;
            Charged(true);
        }

        public void Discharge()
        {
            Energy = 0f;
            Charged(false);
        }

        public void EnergyWaste(bool isBoosted)
        {
            if (!InEnoughEnergy)
                return;

            var wasteOfEnergy = isBoosted ? boostedWasteOfEnergyInSeconds : defaultWasteOfEnergyInSeconds;
            Energy -= Time.deltaTime / (wasteOfEnergy + 0.001f);

            if (!InEnoughEnergy)
                Discharge();
        }
    }

    [Serializable]
    public class AirplaneModularSystem
    {
        public bool ModuleExists => _modules.Count > 0;

        List<Module> _modules = new List<Module>();

        public void AddModule(Module module)
        {
            _modules.Add(module);
            HUDManager.Instance.ModulesHUD.AddModuleUI(module);

            Debug.Log("Module added.");
        }

        public void UseModule(Airplane airplane, int index)
        {
            _modules[index].Use(airplane);
            _modules.RemoveAt(index);
            HUDManager.Instance.ModulesHUD.RemoveModuleUI(index);

            Debug.Log("Module used.");
        }
    }
}