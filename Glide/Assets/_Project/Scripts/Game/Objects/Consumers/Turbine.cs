using UnityEngine;

namespace Gisha.Glide.Game.Objects.Consumers
{
    public class Turbine : EnergyConsumer
    {
        [Header("General")]
        [SerializeField] private float angularSpeed = default;

        private void Update()
        {
            if (IsWorking)
                transform.Rotate(Vector3.forward * angularSpeed * Time.deltaTime);
        }
    }
}