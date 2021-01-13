using Gisha.Glide.Game.Core;
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

        float lastTime = 0;
        private void OnTriggerEnter(Collider other)
        {
            var time = Time.time;
            if (other.CompareTag("Airplane") && (time - lastTime) > 0.25f)
            {
                var airplane = other.GetComponentInParent<Airplane>();
                var module = ModulesStorage.GetModuleFromTypeName(collectibleModuleTypeName);
                airplane.modularSystem.AddModule(module);

                ScoreProcessor.AddScore(25);
                Destroy(gameObject);

                lastTime = Time.time;
            }
        }
    }
}