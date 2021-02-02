using UnityEngine;

namespace Gisha.Glide.Game.Objects
{
    public class Accumulator : MonoBehaviour
    {
        [SerializeField] private Turbine[] turbines = default;

        public bool IsActive { get; private set; } = true;

        private void Awake()
        {
            foreach (var turbine in turbines)
                turbine.Initialize(this);
        }

        public void Destroy()
        {
            IsActive = false;
        }

        private void OnDrawGizmos()
        {
            if (IsActive)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 5f);
        }
    }
}