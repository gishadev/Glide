using Gisha.Glide.Game.Core;
using System.Collections;
using UnityEngine;

namespace Gisha.Glide.Game.AirplaneGeneric.Modules
{
    public class ModuleCollectible : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private string collectibleModuleTypeName = default;
        [Header("Pickup")]
        [SerializeField] private float pickupRadius = default;
        [SerializeField] private float flySpeed = default;

        private void Awake()
        {
            if (string.IsNullOrEmpty(collectibleModuleTypeName)) 
                Debug.LogError("collectibleModuleTypeName is not assigned!");
        }

        private void Start()
        {
            var airplaneLayerMask = 1 << LayerMask.NameToLayer("Airplane");
            StartCoroutine(CheckForAirplaneCoroutine(airplaneLayerMask));
        }

        #region Coroutines
        private IEnumerator CheckForAirplaneCoroutine(int airplaneLayerMask)
        {
            while (true)
            {
                if (Physics.CheckSphere(transform.position, pickupRadius, airplaneLayerMask))
                {
                    StartCoroutine(MoveTowardsAirplane());
                    break;
                }

                yield return null;
            }
        }   

        private IEnumerator MoveTowardsAirplane()
        {
            var airplane = AirplaneSpawner.Instance.Airplane;

            while (true)
            {
                var delta = flySpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, airplane.transform.position, delta);
                yield return null;
            }
        }

        #endregion

        float lastTime = 0;
        private void OnTriggerEnter(Collider other)
        {
            var time = Time.time;
            if (other.CompareTag("Airplane") && (time - lastTime) > 0.25f)
            {
                StopAllCoroutines();

                var airplane = other.GetComponentInParent<Airplane>();
                var module = ModulesStorage.GetModuleFromTypeName(collectibleModuleTypeName);
                airplane.modularSystem.AddModule(module);

                ScoreProcessor.AddScore(25);
                Destroy(gameObject);

                lastTime = Time.time;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, pickupRadius);
        }
    }
}