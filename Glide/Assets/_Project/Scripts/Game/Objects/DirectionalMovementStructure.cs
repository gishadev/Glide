using UnityEngine;

namespace Gisha.Glide.Game.Objects
{
    public class DirectionalMovementStructure : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private Transform dynamicTrans = default;
        [Space]
        [SerializeField] private float speed = default;
        [Space]
        [SerializeField] private float maxDistFromCenter = default;
        [SerializeField] private MovementDirection movementDirection = default;
        public enum MovementDirection { Horizontal, Vertical }

        Transform _transform;

        Vector3 _direction;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            if (maxDistFromCenter == 0f)
                return;

            if (movementDirection == MovementDirection.Horizontal)
                _direction = Vector3.right;
            if (movementDirection == MovementDirection.Vertical)
                _direction = Vector3.up;

            if (Random.Range(0, 100) <= 50)
                SetOppositeDirection(_direction);
        }

        private void Update()
        {
            if (maxDistFromCenter == 0f)
                return;

            var oldDir = dynamicTrans.position - _transform.position;
            if (oldDir.magnitude > maxDistFromCenter)
                SetOppositeDirection(oldDir);

            dynamicTrans.Translate(_direction * speed * Time.deltaTime);
        }

        private void SetOppositeDirection(Vector3 oldDir)
        {
            if (movementDirection == MovementDirection.Horizontal)
                _direction = Vector3.right * -Mathf.Sign(oldDir.x);
            if (movementDirection == MovementDirection.Vertical)
                _direction = Vector3.up * -Mathf.Sign(oldDir.y);
        }

        private void OnValidate()
        {
            if (movementDirection == MovementDirection.Horizontal)
                _direction = Vector3.right;
            if (movementDirection == MovementDirection.Vertical)
                _direction = Vector3.up;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Vector3 ray = dynamicTrans.TransformDirection(_direction * maxDistFromCenter);
            Gizmos.DrawWireSphere(transform.position + ray, 35f);
            Gizmos.DrawWireSphere(transform.position - ray, 35f);
        }
    }
}