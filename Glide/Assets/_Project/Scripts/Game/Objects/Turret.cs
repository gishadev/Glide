using Gisha.Glide.Game.AirplaneGeneric;
using System.Collections;
using UnityEngine;

namespace Gisha.Glide.Game.Objects
{
    public class Turret : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private GameObject projectilePrefab = default;
        [SerializeField] private float shootingDelay = default;

        Collider _collider;
        Transform _transform;
        Airplane _airplane;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _transform = transform;
        }

        private void Start()
        {
            StartCoroutine(ShootingCoroutine());
            _airplane = AirplaneSpawner.Instance.Airplane;
        }

        private void Update()
        {
            LookAtAirplane();
        }

        private IEnumerator ShootingCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(shootingDelay);

                var layerMask = 1 << LayerMask.NameToLayer("Airplane");
                if (Physics.Raycast(_transform.position, _transform.forward, out RaycastHit hitInfo, 2500f, layerMask))
                {
                    if (hitInfo.collider.CompareTag("Airplane"))
                        Shoot();

                    Debug.Log(hitInfo.collider.name);
                }

            }
        }

        private void Shoot()
        {
             GameObject.Instantiate(projectilePrefab, _transform.position, _transform.rotation);
        }

        private void LookAtAirplane()
        {
            _transform.LookAt(_airplane.transform);
        }
    }
}