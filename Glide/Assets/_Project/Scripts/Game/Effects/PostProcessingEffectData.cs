using UnityEngine;

namespace Gisha.Glide.Game.Effects
{
    [CreateAssetMenu(fileName = "PostProcessingEffectData", menuName = "Scriptable Objects/Effects/Create PostProcessingEffectData", order = 0)]
    public class PostProcessingEffectData : ScriptableObject
    {
        [Header("Chromatic Aberration")]
        [SerializeField] private float maxCA = default;
        [SerializeField] private float minCA = default;

        [Header("Lens Distortion")]
        [SerializeField] private float maxLD = default;
        [SerializeField] private float minLD = default;

        public float MaxCA => maxCA;
        public float MinCA => minCA;
        public float MaxLD => maxLD;
        public float MinLD => minLD;
    }
}