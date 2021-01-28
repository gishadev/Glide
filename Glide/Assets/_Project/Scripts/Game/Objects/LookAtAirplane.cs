using Gisha.Glide.Game.AirplaneGeneric;
using Gisha.Glide.Game.Core.SceneLoading;
using UnityEngine;

namespace Gisha.Glide.Game.Objects
{
    [RequireComponent(typeof(TriggerableObject))]
    public class LookAtAirplane : MonoBehaviour
    {
        [Header("Rotate Toward Airplane")]
        [SerializeField] private Transform rotationTarget = default;
        [SerializeField] private float minDistToRotateTowardsAirplane = default;

        Airplane _airplane;
        TriggerableObject _trigger;

        private void Start()
        {
            _airplane = AirplaneSpawner.Instance.Airplane;
            _trigger = GetComponent<TriggerableObject>();
        }

        private void Update()
        {
            if (LoadingManager.IsLoading)
                return;

            if (!_trigger.IsTriggered && Vector3.Distance(rotationTarget.position, _airplane.transform.position) > minDistToRotateTowardsAirplane)
                LootAtAirplane();
        }

        private void LootAtAirplane()
        {
            var forward = (_airplane.transform.position - rotationTarget.position).normalized;
            var upwards = Vector3.up;
            rotationTarget.rotation = Quaternion.LookRotation(forward, upwards);
        }
    }
}