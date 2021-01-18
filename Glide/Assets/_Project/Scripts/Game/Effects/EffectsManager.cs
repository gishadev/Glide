using UnityEngine;
using Cinemachine;
using Gisha.Glide.Game.AirplaneGeneric;
using System.Collections;

namespace Gisha.Glide.Game.Effects
{
    public class EffectsManager : MonoBehaviour
    {
        #region Singleton
        public static EffectsManager Instance { get; private set; }
        #endregion

        [Header("Camera Effect")]
        [SerializeField] private CinemachineVirtualCamera virtualCamera = default;
        [SerializeField] private CameraEffectData cameraEffectData = default;


        private void Awake()
        {
            Instance = this;

            CameraEffect.Initialize(virtualCamera, cameraEffectData);
        }
    }

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
}