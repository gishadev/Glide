using Gisha.Glide.Game.AirplaneGeneric.Modules;
using System.Collections.Generic;
using UnityEngine;

namespace Gisha.Glide.Game.AirplaneGeneric.HUD
{
    public class ModulesHUD : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private Transform modulesParent = default;
        [SerializeField] private GameObject moduleUIPrefab = default;

        List<ModuleUI> _modulesUI = new List<ModuleUI>();

        public void AddModuleUI(Module module)
        {
            var moduleUI = Instantiate(moduleUIPrefab, modulesParent).GetComponent<ModuleUI>();
            moduleUI.Initialize(module.GetModuleUIData().IconSprite);
            _modulesUI.Add(moduleUI);
        }

        public void RemoveModuleUI(int index)
        {
            Destroy(_modulesUI[index].gameObject);
            _modulesUI.RemoveAt(index);
        }
    }
}