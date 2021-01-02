using UnityEngine;
using Cinemachine;
using Gisha.Glide.AirplaneGeneric;

namespace Gisha.Glide
{
    public class CameraController : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField] private CinemachineVirtualCamera virtualCamera = default;

        [Header("FOV Changing")]
        [SerializeField] private float changingSpeed = default;
        [SerializeField] private float defaultFOV = 60f;
        [SerializeField] private float boostFOV = default;

        float _fov;

        private void Start()
        {
            _fov = defaultFOV;
        }

        private void Update()
        {
            float fovTarget = AirplaneSpawner.Instance.Airplane.IsBoostedSpeed ? boostFOV : defaultFOV;

            _fov = Mathf.Lerp(_fov, fovTarget, Time.deltaTime * changingSpeed);
            virtualCamera.m_Lens.FieldOfView = _fov;
        }
    }
}