using Gisha.Glide.Game.AirplaneGeneric.Modules;
using UnityEngine;

namespace Gisha.Glide.Game.AirplaneGeneric.HUD
{
    public class ModulesHUD : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private Transform modulesParent = default;
        [SerializeField] private GameObject moduleUIPrefab = default;

        GameObject _createdModuleUI;

        private void OnEnable()
        {
            Airplane.OnAddModule += AddModuleUI;
            Airplane.OnUseModule += RemoveModuleUI;
        }

        private void OnDisable()
        {
            Airplane.OnAddModule -= AddModuleUI;
            Airplane.OnUseModule -= RemoveModuleUI;
        }

        private void AddModuleUI(Module module)
        {
            _createdModuleUI = Instantiate(moduleUIPrefab, modulesParent);
        }

        private void RemoveModuleUI(Module module)
        {
            Destroy(_createdModuleUI);
        }
    }
}