using Gisha.Glide.Game.AirplaneGeneric;
using System.Collections;
using UnityEngine;

namespace Gisha.Glide.Game.Objects.Consumers
{
    public class Turret : EnergyConsumer
    {
        [Header("General")]
        [SerializeField] private GameObject projectilePrefab = default;
        [SerializeField] private float shootingDelay = default;

        Airplane _airplane;

        private void Start()
        {
            StartCoroutine(ShootingCoroutine());
            _airplane = AirplaneSpawner.Instance.Airplane;
        }

        private void Update()
        {
            if (IsWorking)
                LookAtAirplane();
        }

        private IEnumerator ShootingCoroutine()
        {
            while (true)
            {
                yield return new WaitUntil(() => IsWorking);
                yield return new WaitForSeconds(shootingDelay);                    

                var layerMask = 1 << LayerMask.NameToLayer("Airplane");
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 10000f, layerMask))
                {
                    if (hitInfo.collider.CompareTag("Airplane"))
                        Shoot();

                    Debug.Log(hitInfo.collider.name);
                }
            }
        }

        private void Shoot() => Instantiate(projectilePrefab, transform.position, transform.rotation);
        private void LookAtAirplane() => transform.LookAt(_airplane.transform);
    }
}