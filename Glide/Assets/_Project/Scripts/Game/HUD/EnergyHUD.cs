using Gisha.Glide.Game.AirplaneGeneric;
using UnityEngine;
using UnityEngine.UI;

namespace Gisha.Glide.Game.HUD
{
    public class EnergyHUD : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Airplane airplane = default;
        [SerializeField] private Image energyFillImage = default;

        [Header("Colors")]
        [SerializeField] private Color maxEnergyColor = default;
        [SerializeField] private Color medEnergyColor = default;
        [SerializeField] private Color minEnergyColor = default;
        [Space]
        [SerializeField] [Range(0f, 1f)] private float alpha = default;

        private void Awake()
        {
            if (airplane == null) Debug.LogError("airplane is not assigned.");
            if (energyFillImage == null) Debug.LogError("energyFillImage is not assigned.");
        }

        private void OnValidate()
        {
            if (Application.isEditor && !Application.isPlaying)
            {
                maxEnergyColor.a = alpha;
                medEnergyColor.a = alpha;
                minEnergyColor.a = alpha;

                energyFillImage.color = maxEnergyColor;
            }
        }

        private void Update()
        {
            var energyPercentage = airplane.chargeController.Energy;

            if (airplane.chargeController.InEnoughEnergy)
                energyFillImage.transform.localScale = new Vector3(1f, energyPercentage, 1f);

            if (energyPercentage >= 0.5f)
                energyFillImage.color = Color.Lerp(medEnergyColor, maxEnergyColor, Mathf.InverseLerp(0.5f, 1f, energyPercentage));
            else
                energyFillImage.color = Color.Lerp(minEnergyColor, medEnergyColor, Mathf.InverseLerp(0f, 0.5f, energyPercentage));
        }
    }
}