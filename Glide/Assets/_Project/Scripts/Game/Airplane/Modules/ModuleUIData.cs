using UnityEngine;

namespace Gisha.Glide.Game.AirplaneGeneric.Modules
{
    [CreateAssetMenu(fileName = "ModuleUIData", menuName = "Scriptable Objects/Create ModuleUIData", order = 3)]
    public class ModuleUIData : ScriptableObject
    {
        [SerializeField] private Sprite iconSprite = default;

        public Sprite IconSprite => iconSprite;
    }
}