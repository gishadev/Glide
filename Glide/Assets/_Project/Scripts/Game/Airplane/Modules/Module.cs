using Gisha.Glide.Game.Core;
using UnityEngine;

namespace Gisha.Glide.Game.AirplaneGeneric.Modules
{
    public abstract class Module
    {
        public abstract void Use(Airplane airplane);
        public ModulesData GetModulesData() => PathBuilder.GetModulesData();

        public ModuleUIData GetModuleUIData() => PathBuilder.GetModuleUIData(this.GetType().Name);
    }


    [CreateAssetMenu(fileName = "ModuleUIData", menuName = "Scriptable Objects/Create ModuleUIData", order = 3)]
    public class ModuleUIData : ScriptableObject
    {
        [SerializeField] private Sprite iconSprite = default;

        public Sprite IconSprite => iconSprite;
    }
}