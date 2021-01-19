using Gisha.Glide.Game.AirplaneGeneric;
using Gisha.Glide.Game.Core;
using UnityEngine;

namespace Gisha.Glide.Game.Objects
{
    public class Charger : TriggerableObject
    {
        [Header("Charger")]
        [SerializeField] private GameObject chargingGate = default;

        public override void OnTriggerSignal(Collider other)
        {
            other.GetComponentInParent<Airplane>().chargeController.ChargeUp();
            ScoreProcessor.AddScore(50);
            chargingGate.SetActive(false);
        }
    }
}