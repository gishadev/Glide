using UnityEngine;

namespace Gisha.Glide.Objects
{
    public abstract class TriggerableObject : MonoBehaviour
    {
        [Header("Triggerable Object Settings")]
        [SerializeField] private TriggerSignal triggerSignal = default;

        private void OnEnable()
        {
            triggerSignal.OnTriggerSignal += OnTriggerSignal;
        }

        private void OnDisable()
        {
            triggerSignal.OnTriggerSignal -= OnTriggerSignal;
        }

        public abstract void OnTriggerSignal(Collider other);
    }
}