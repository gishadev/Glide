using Gisha.Glide.Game.AirplaneGeneric;
using Gisha.Glide.Game.Core;
using UnityEngine;

namespace Gisha.Glide.Game.Objects
{
    public class Charger : TriggerableObject
    {
        [Header("Charger")]
        [SerializeField] private GameObject chargingGate = default;
        [SerializeField] private float minDistToRotatateTowardsAirplane = default;

        Transform _transform;
        Airplane _airplane;

        public override void OnTriggerSignal(Collider other)
        {
            other.GetComponentInParent<Airplane>().chargeController.ChargeUp();
            ScoreProcessor.AddScore(50);
            chargingGate.SetActive(false);
        }

        private void Start()
        {
            _transform = transform;
            _airplane = AirplaneSpawner.Instance.Airplane;
        }

        private void Update()
        {
            if (!IsTriggered && Vector3.Distance(_transform.position, _airplane.transform.position) > minDistToRotatateTowardsAirplane)
                LootAtAirplane();
        }

        private void LootAtAirplane()
        {
            var forward = (_airplane.transform.position - _transform.position).normalized;
            var upwards = Vector3.up;
            _transform.rotation = Quaternion.LookRotation(forward, upwards);
        }
    }
}