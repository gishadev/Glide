using UnityEngine;

namespace Gisha.Glide.Game.Effects
{
    [CreateAssetMenu(fileName = "CameraEffectData", menuName = "Scriptable Objects/Effects/Create CameraEffectData")]
    public class CameraEffectData : ScriptableObject
    {
        [Header("FOV Changing")]
        [SerializeField] private float changingSpeed = 5f;
        [SerializeField] private float defaultFOV = 65f;
        [SerializeField] private float boostFOV = 80;
        [SerializeField] private float dashFOV = 85;

        public float ChangingSpeed => changingSpeed;
        public float DefaultFOV => defaultFOV;
        public float BoostFOV => boostFOV;
        public float DashFOV => dashFOV;
    }
}