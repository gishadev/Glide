using UnityEngine;

namespace Gisha.Glide.Plane
{
    public class PlaneMovement : MonoBehaviour
    {
        [Header("Speeds")]
        [SerializeField] private float forwardSpeed = 25f;
        [SerializeField] private float rollSpeed = 5;

        float _nowForwardSpeed, _nowRollSpeed;

        [Header("Accelerations")]
        [SerializeField] private float forwardAcceleration = 2.5f;
        [SerializeField] private float rollAcceleration = 2.5f;

        [Header("Look Rotation")]
        [SerializeField] private float lookRotateSpeed = 90f;

        Vector2 lookInput, screenCenter, mouseDist;

        Vector3 _velocity;
        Rigidbody _rb;
        Transform _transform;

        private void Awake()
        {
            _transform = transform;
             _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            screenCenter.x = Screen.width / 2f;
            screenCenter.y = Screen.height / 2f;

            Cursor.lockState = CursorLockMode.Confined;
        }

        private void FixedUpdate()
        {
            GetMouseDistFromCenter();
            RotatePlane();

            ForwardVelocity();
        }

        void ForwardVelocity()
        {
            _nowForwardSpeed = Mathf.Lerp(_nowForwardSpeed, forwardSpeed, Time.deltaTime * forwardAcceleration);

            _velocity = transform.forward * _nowForwardSpeed * Time.deltaTime;
            _rb.position += _velocity;
        }

        void RotatePlane()
        {
            var xAngle = -mouseDist.y * lookRotateSpeed * Time.deltaTime;
            var yAngle = mouseDist.x * lookRotateSpeed * Time.deltaTime;

            _nowRollSpeed = Mathf.Lerp(_nowRollSpeed, -Input.GetAxisRaw("Horizontal") * rollSpeed, Time.deltaTime * rollAcceleration);
            var zAngle = _nowRollSpeed * Time.deltaTime;

            _transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
        }

        private void GetMouseDistFromCenter()
        {
            lookInput.x = Input.mousePosition.x;
            lookInput.y = Input.mousePosition.y;

            mouseDist.x = (lookInput.x - screenCenter.x) / screenCenter.y;
            mouseDist.y = (lookInput.y - screenCenter.y) / screenCenter.y;

            mouseDist = Vector2.ClampMagnitude(mouseDist, 1f);
        }
    }
}