using UnityEngine;

namespace Gisha.Glide.Game.Objects
{
    public class Turbine : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private float angularSpeed = default;

        Transform _transform;

        bool _isInitialized = false;
        Accumulator _accumulator;

        private void Update()
        {
            if (_isInitialized && _accumulator.IsActive)
                _transform.Rotate(Vector3.forward * angularSpeed * Time.deltaTime);
        }

        public void Initialize(Accumulator accumulator)
        {
            _transform = transform;
            _accumulator = accumulator;

            _isInitialized = true;
        }
    }
}