using Gisha.Glide.AirplaneGeneric;
using UnityEngine;

namespace Gisha.Glide.Objects
{
    public class Charger : TriggerableObject
    {
        [Header("Charger")]
        [SerializeField] private GameObject chargingGate = default;

        public override void OnTriggerSignal(Collider other)
        {
            other.GetComponentInParent<Airplane>().ChargeUp();
            chargingGate.SetActive(false);
        }
    }
}