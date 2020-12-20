using UnityEngine;

namespace Gisha.Glide.Plane
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Following Settings")]
        [SerializeField] private Transform followTarget = default;
        [SerializeField] private float followSpeed = default;
        [SerializeField] private Vector3 followOffset = default;

        Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void FixedUpdate()
        {
            Following();
        }

        void Following()
        {
            var oldPos = _transform.position;
            var newPos = new Vector3(followTarget.position.x, followTarget.position.y, followTarget.position.z) + followOffset; 

            _transform.position = Vector3.Lerp(oldPos, newPos, followSpeed * Time.deltaTime);
        }
    }
}