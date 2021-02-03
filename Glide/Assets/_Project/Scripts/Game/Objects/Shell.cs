using UnityEngine;

namespace Gisha.Glide.Game.Objects
{
    public class Shell : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private float flyingSpeed = default;
        [SerializeField] private float lifeTime = default;

        Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            Invoke("Explode", lifeTime);
        }

        private void FixedUpdate()
        {
            _rb.velocity = transform.forward * flyingSpeed * Time.fixedDeltaTime;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Obstacle") || collision.collider.CompareTag("Airplane"))
                Explode();
        }

        private void Explode()
        {
            Destroy(gameObject);
        }
    }
}