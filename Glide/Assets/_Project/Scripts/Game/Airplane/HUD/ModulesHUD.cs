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

        List<GameObject> _modulesUI = new List<GameObject>();

        public void AddModuleUI(Module module)
        {
            _modulesUI.Add(Instantiate(moduleUIPrefab, modulesParent));
        }

        public void RemoveModuleUI(int index)
        {
            Destroy(_modulesUI[index]);
            _modulesUI.RemoveAt(index);
        }
    }
}