using UnityEngine;

namespace Gisha.Glide.Level
{
    [CreateAssetMenu(fileName = "Level Settings", menuName = "Scriptable Objects/Create Level Settings", order = 0)]
    public class LevelSettings : ScriptableObject
    {
        [Header("Colors")]
        [SerializeField] private Material tunnelMaterial = default;

        public Material TunnelMaterial => tunnelMaterial;
    }
}