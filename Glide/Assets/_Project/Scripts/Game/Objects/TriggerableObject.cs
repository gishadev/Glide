using UnityEngine;

namespace Gisha.Glide.Game.Objects
{
    public abstract class TriggerableObject : MonoBehaviour
    {
        [Header("Triggerable Object Settings")]
        [SerializeField] private TriggerSignal triggerSignal = default;

        public bool IsTriggered { get; private set; } = false;

        private void OnEnable() => triggerSignal.OnTriggerSignal += Trigger;
        private void OnDisable() => triggerSignal.OnTriggerSignal -= Trigger;

        public abstract void OnTriggerSignal(Collider other);

        private void Trigger(Collider other)
        {
            IsTriggered = true;
            OnTriggerSignal(other);
        }
    }
}