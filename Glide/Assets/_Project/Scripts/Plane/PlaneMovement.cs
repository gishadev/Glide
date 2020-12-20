using UnityEngine;

namespace Gisha.Glide.Plane
{
    public class PlaneMovement : MonoBehaviour
    {
        [Header("Speeds")]
        [SerializeField] private float forwardSpeed = 25f;
        [SerializeField] private float strafeSpeed = 7.5f;
        [SerializeField] private float hoverSpeed = 5f;

        float _nowForwardSpeed, _nowStrafeSpeed, _nowHoverSpeed;

        [Header("Accelerations")]
        [SerializeField] private float forwardAcceleration = 2.5f;
        [SerializeField] private float strafeAcceleration = 2f;
        [SerializeField] private float hoverAcceleration = 2f;

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

            GetMovementSpeeds();
            UpdateVelocity();
        }

        void UpdateVelocity()
        {
            var rightVel = transform.right * _nowStrafeSpeed * Time.deltaTime;
            var upVel = transform.up * _nowHoverSpeed * Time.deltaTime;
            var forwardVel = transform.forward * _nowForwardSpeed * Time.deltaTime;
            _velocity = rightVel + upVel + forwardVel;

            _rb.position += _velocity;
        }

        void GetMovementSpeeds()
        {
            var newForwardSpeed = forwardSpeed;
            _nowForwardSpeed = Mathf.Lerp(_nowForwardSpeed, newForwardSpeed, Time.deltaTime * forwardAcceleration);

            var newStrafeSpeed = Input.GetAxisRaw("Horizontal") * strafeSpeed;
            _nowStrafeSpeed = Mathf.Lerp(_nowStrafeSpeed, newStrafeSpeed, Time.deltaTime * strafeAcceleration);

            var newHoverSpeed = Input.GetAxisRaw("Hover") * hoverSpeed;
            _nowHoverSpeed = Mathf.Lerp(_nowHoverSpeed, newHoverSpeed, Time.deltaTime * hoverAcceleration);
        }

        void RotatePlane()
        {
            var xAngle = -mouseDist.y * lookRotateSpeed * Time.deltaTime;
            var yAngle = mouseDist.x * lookRotateSpeed * Time.deltaTime;
            _transform.Rotate(xAngle, yAngle, 0f, Space.Self);
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