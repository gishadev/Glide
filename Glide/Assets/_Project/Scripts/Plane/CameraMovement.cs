using UnityEngine;

namespace Gisha.Glide.Plane
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Following Settings")]
        [SerializeField] private Transform followTarget = default;
        [SerializeField] private float followSpeed = default;
        [SerializeField] private float zOffset = default;

        Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void FixedUpdate()
        {
            ForwardFollowing();
        }

        void ForwardFollowing()
        {
            var oldPos = _transform.position;
            var newPos = new Vector3(oldPos.x, oldPos.y, followTarget.position.z) + zOffset * Vector3.forward; 

            _transform.position = Vector3.Lerp(oldPos, newPos, followSpeed * Time.deltaTime);
        }
    }
}