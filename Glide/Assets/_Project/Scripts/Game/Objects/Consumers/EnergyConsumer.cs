using UnityEngine;

namespace Gisha.Glide.Game.Objects.Consumers
{
    public abstract class EnergyConsumer : MonoBehaviour
    {
        public bool IsWorking => _accumulator != null && _accumulator.IsActive && _isInitialized;

        bool _isInitialized = false;
        Accumulator _accumulator;

        private void OnEnable()
        {
            if (!_isInitialized)
                Debug.LogError($"<color=yellow>EnergyConsumer: {name}</color> is not initialized!");
        }

        public void Initialize(Accumulator accumulator)
        {
            _accumulator = accumulator;
            _isInitialized = true;
        }
    }
}