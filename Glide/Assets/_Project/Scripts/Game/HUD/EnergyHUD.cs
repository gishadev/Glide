using Gisha.Glide.Game.AirplaneGeneric;
using UnityEngine;

namespace Gisha.Glide.Game.HUD
{
    public class EnergyHUD : MonoBehaviour
    {
        [SerializeField] private Airplane airplane = default;
        [SerializeField] private Transform energyFillTrans = default;

        private void Update()
        {
            if (airplane.chargeController.InEnoughEnergy)
                energyFillTrans.localScale = new Vector3(airplane.chargeController.Energy, 1f, 1f);
        }
    }
}