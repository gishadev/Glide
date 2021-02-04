using Gisha.Glide.Game.Objects;
using UnityEngine;

namespace Gisha.Glide.Utility
{
    public class Projectile : MonoBehaviour
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
            if (collision.collider.CompareTag("Obstacle") || collision.collider.CompareTag("Airplane"))
            {
                Explode();

                if (collision.collider.TryGetComponent(out Accumulator accumulator))
                    accumulator.Destroy();
            }
        }

        private void Explode()
        {
            if (explosionEffect != null)
                Instantiate(explosionEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}