using UnityEngine;

namespace Gisha.Glide.Plane
{
    public class PlaneMovement : MonoBehaviour
    {
        [SerializeField] private float forwardMovement = default;

        Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();   
        }

        private void Update()
        {
            ForwardMovement();
        }

        void ForwardMovement()
        {
            _rb.velocity = Vector3.forward * forwardMovement * Time.fixedDeltaTime;
        }
    }
}