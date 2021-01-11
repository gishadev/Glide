using System;
using UnityEngine;

namespace Gisha.Glide.Game.AirplaneGeneric.Modules
{
    public class ModuleCollectible : MonoBehaviour
    {
        [SerializeField] private CollectibleType collectibleType = default;

        private enum CollectibleType
        {
            Nothing,
            DashModule
        }

        private void Awake()
        {
            if (collectibleType == CollectibleType.Nothing) Debug.LogError("collectibleType is not assigned!");
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Airplane"))
            {
                var airplane = other.GetComponentInParent<Airplane>();

                switch (collectibleType)
                {
                    case CollectibleType.DashModule:
                        airplane.AddModule(new DashModule());
                        break;
                }

                Destroy(gameObject);
            }
        }
    }
}