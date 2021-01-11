using System;
using UnityEngine;

namespace Gisha.Glide.Game.AirplaneGeneric.Modules
{
    public class ModuleCollectible : MonoBehaviour
    {
        [SerializeField] private string collectibleModuleTypeName = default;

        private void Awake()
        {
            if (string.IsNullOrEmpty(collectibleModuleTypeName)) Debug.LogError("collectibleModuleTypeName is not assigned!");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Airplane"))
            {
                var airplane = other.GetComponentInParent<Airplane>();
                var module = ModulesStorage.GetModuleFromTypeName(collectibleModuleTypeName);
                airplane.AddModule(module);

                Destroy(gameObject);
            }
        }
    }
}