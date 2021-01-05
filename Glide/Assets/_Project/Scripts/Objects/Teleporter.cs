using Gisha.Glide.AirplaneGeneric;
using Gisha.Glide.Game;
using UnityEngine;

namespace Gisha.Glide.Objects
{
    public class Teleporter : TriggerableObject
    {
        [Header("Teleporter")]
        [SerializeField] private float minDistToRotatateTowardsAirplane = default;

        Transform _transform;
        Airplane _airplane;

        public override void OnTriggerSignal(Collider other)
        {
            Debug.Log("<color=green>Airplane was teleported!</color>");
            SceneLoader.LoadNextLevel();
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