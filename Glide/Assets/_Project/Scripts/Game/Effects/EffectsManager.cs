using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Gisha.Glide.Game.Effects
{
    #region EffectsManager
    public class EffectsManager : MonoBehaviour
    {
        #region Singleton
        public static EffectsManager Instance { get; private set; }
        #endregion

        [Header("Camera Effect")]
        [SerializeField] private CinemachineVirtualCamera virtualCamera = default;
        [SerializeField] private CameraEffectData cameraEffectData = default;

        [Header("Post-Processing Effect")]
        [SerializeField] private Volume volume;
        [SerializeField] private PostProcessingEffectData postProcessingEffectData = default;
        private void Awake()
        {
            Instance = this;

            CameraEffect.Initialize(virtualCamera, cameraEffectData);
            PostProcessingEffect.Initialize(volume, postProcessingEffectData);
        }
    }
    #endregion
    //-------------------------------------------------------------------------
    #region CameraEffect
    public static class CameraEffect
    {
        public enum CameraEffectState
        {
            Default,
            Boosted,
            Dash
        }

        static float _FOV;
        static CinemachineVirtualCamera _virtualCamera;
        static CameraEffectData _cameraEffectData;

        public static void Initialize(CinemachineVirtualCamera virtualCamera, CameraEffectData cameraEffectData)
        {
            _virtualCamera = virtualCamera;
            _cameraEffectData = cameraEffectData;

            _FOV = _cameraEffectData.DefaultFOV;
        }

        public static void ChangeFOV(CameraEffectState state)
        {
            var target = _FOV;

            switch (state)
            {
                case CameraEffectState.Default:
                    target = _cameraEffectData.DefaultFOV;
                    break;
                case CameraEffectState.Boosted:
                    target = _cameraEffectData.BoostFOV;
                    break;
                case CameraEffectState.Dash:
                    target = _cameraEffectData.DashFOV;
                    break;
            }

            EffectsManager.Instance.StopAllCoroutines();
            EffectsManager.Instance.StartCoroutine(FOVChangingCoroutine(target));
        }

        private static IEnumerator FOVChangingCoroutine(float targetFOV)
        {
            bool isIncreasing = _FOV < targetFOV;
            float velocity = 0;
            float offset = isIncreasing ? 1f : -1f;
            while ((isIncreasing && _FOV < targetFOV) || (!isIncreasing && _FOV > targetFOV))
            {
                _FOV = Mathf.SmoothDamp(_FOV, targetFOV + offset, ref velocity, Time.deltaTime * _cameraEffectData.ChangingSpeed);
                _virtualCamera.m_Lens.FieldOfView = _FOV;
                yield return null;
            }
            _FOV = targetFOV;
            _virtualCamera.m_Lens.FieldOfView = _FOV;
        }
    }
    #endregion
    //-------------------------------------------------------------------------
    #region PostProcessingEffect
    public static class PostProcessingEffect
    {
        static PostProcessingEffectData _postProcessingEffectData;
        static Volume _volume;

        static LensDistortion _ld = null;
        static ChromaticAberration _ca = null;

        public static void Initialize(Volume volume, PostProcessingEffectData postProcessingEffectData)
        {
            _postProcessingEffectData = postProcessingEffectData;
            _volume = volume;

            volume.sharedProfile.TryGet(out _ld);
            volume.sharedProfile.TryGet(out _ca);

            SetChromaticAberration(0f);
            SetLensDistortion(0f);
        }

        public static void SetChromaticAberration(float intensity)
        {
            _ca.intensity.value = Mathf.Clamp(intensity, _postProcessingEffectData.MinCA, _postProcessingEffectData.MaxCA);
        }

        public static void SetLensDistortion(float intensity)
        {
            _ld.intensity.value = Mathf.Clamp(intensity, _postProcessingEffectData.MinLD, _postProcessingEffectData.MaxLD);
        }
    }
    #endregion
}