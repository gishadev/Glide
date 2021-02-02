using Gisha.Glide.Game.Objects;
using UnityEngine;

namespace Gisha.Glide.Game.AirplaneGeneric.Modules.Extra
{
    public class Rocket : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private float flyingSpeed = default;
        [SerializeField] private GameObject explosionEffect = default;

        Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _rb.velocity = transform.forward * flyingSpeed * Time.fixedDeltaTime;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Obstacle"))
            {
                Explode();

                if (collision.collider.TryGetComponent(out Accumulator accumulator))
                    accumulator.Destroy();
            }
        }

        private void Explode()
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}