using UnityEngine;

namespace Gisha.Glide.AirplaneGeneric
{
    public class EnergyHUD : MonoBehaviour
    {
        [SerializeField] private Airplane airplane = default;
        [SerializeField] private Transform energyFillTrans = default;

        private void Update()
        {
            if (airplane.IsCharged)
                energyFillTrans.localScale = new Vector3(airplane.Energy, 1f, 1f);
        }
    }
}