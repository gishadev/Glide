using UnityEngine;

namespace Gisha.Glide.AirplaneGeneric.Modules
{
    [CreateAssetMenu(fileName = "ModulesData", menuName = "Scriptable Objects/Create Modules Data", order = 2)]
    public class ModulesData : ScriptableObject
    {
        [Header("Dash")]
        [SerializeField] private float dashDistance = default;

        public float DashDistance => dashDistance;

    }
}