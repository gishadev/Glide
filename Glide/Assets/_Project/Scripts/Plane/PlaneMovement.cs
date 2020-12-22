using UnityEngine;

namespace Gisha.Glide.Plane
{
    public class PlaneMovement : MonoBehaviour
    {
        [Header("Speed")]
        [SerializeField] private float forwardSpeed = 25f;
        [Header("Acceleration")]
        [SerializeField] private float forwardAcceleration = 2.5f;

        float _nowForwardSpeed;

        [Header("Rotation")]
        [SerializeField] private float rotationSpeed = default;
        [SerializeField] private float rotationSens = default;
        [SerializeField] private float aimDistResetSpeed = default;
        [SerializeField] private Transform mouseAimTrans = default;
        [SerializeField] private Transform planeForwardTrans = default;

        [SerializeField] private Transform planeForwardPosition = default;

        Vector2 _lookDelta, _mouseDist, _screenCenter;
        Vector3 _velocity;

        Rigidbody _rb;
        Transform _transform;
        Camera _cam;

        private void Awake()
        {
            _transform = transform;
            _rb = GetComponent<Rigidbody>();
            _cam = Camera.main;
        }

        private void Start()
        {
            _screenCenter.x = Screen.width / 2f;
            _screenCenter.y = Screen.height / 2f;

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void FixedUpdate()
        {
            GetMouseDistance();
            UpdateCanvasTransform();

            RotatePlane();
            ResetMouseDistance();

            ForwardVelocity();
        }

        void ForwardVelocity()
        {
            _nowForwardSpeed = Mathf.Lerp(_nowForwardSpeed, forwardSpeed, Time.deltaTime * forwardAcceleration);

            _velocity = transform.forward * _nowForwardSpeed * Time.deltaTime;
            _rb.position += _velocity;
        }

        void GetMouseDistance()
        {
            _lookDelta.x = Input.GetAxis("Mouse X") * rotationSpeed;
            _lookDelta.y = Input.GetAxis("Mouse Y") * rotationSpeed;

            _mouseDist += _lookDelta;
        }

        void ResetMouseDistance()
        {
            _mouseDist = Vector2.Lerp(_mouseDist, Vector2.zero, aimDistResetSpeed * Time.deltaTime);
        }

        void UpdateCanvasTransform()
        {
            mouseAimTrans.position = _screenCenter + _mouseDist * rotationSens;
            planeForwardTrans.position = _cam.WorldToScreenPoint(planeForwardPosition.position);
        }

        void RotatePlane()
        {
            var xAngle = -_mouseDist.y * Time.deltaTime;
            var yAngle = _mouseDist.x * Time.deltaTime;

            _transform.Rotate(xAngle, yAngle, 0f, Space.World);
        }
    }
}