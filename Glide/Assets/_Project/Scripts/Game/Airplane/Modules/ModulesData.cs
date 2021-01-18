using UnityEngine;

namespace Gisha.Glide.Game.AirplaneGeneric.Modules
{
    [CreateAssetMenu(fileName = "ModulesData", menuName = "Scriptable Objects/Create Modules Data", order = 2)]
    public class ModulesData : ScriptableObject
    {
        [Header("Dash")]
        [SerializeField] private float dashDistance = default;
        [SerializeField] private float dashSpeed = default;

        public float DashDistance => dashDistance;
        public float DashSpeed => dashSpeed;

    }
}