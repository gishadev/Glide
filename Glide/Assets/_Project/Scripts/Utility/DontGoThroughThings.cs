using UnityEngine;

namespace Gisha.Glide.Utility
{
    public class DontGoThroughThings : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private LayerMask layerMask = -1;
        [SerializeField] private float skinWidth = 0.1f;

        float _minimumExtent;
        float _partialExtent;
        float _sqrMinimumExtent;
        Vector3 _previousPosition;

        Rigidbody _rb;
        Collider _collider;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponentInChildren<Collider>();
            _previousPosition = _rb.position;
            _minimumExtent = Mathf.Min(Mathf.Min(_collider.bounds.extents.x, _collider.bounds.extents.y), _collider.bounds.extents.z);
            _partialExtent = _minimumExtent * (1.0f - skinWidth);
            _sqrMinimumExtent = _minimumExtent * _minimumExtent;
        }

        void FixedUpdate()
        {
            //have we moved more than our minimum extent? 
            Vector3 movementThisStep = _rb.position - _previousPosition;
            float movementSqrMagnitude = movementThisStep.sqrMagnitude;

            if (movementSqrMagnitude > _sqrMinimumExtent)
            {
                float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
                RaycastHit hitInfo;

                //check for obstructions we might have missed 
                if (Physics.Raycast(_previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value))
                {
                    if (!hitInfo.collider)
                        return;

                    if (hitInfo.collider.isTrigger)
                        hitInfo.collider.SendMessage("OnTriggerEnter", _collider);

                    if (!hitInfo.collider.isTrigger)
                        _rb.position = hitInfo.point - movementThisStep / movementMagnitude * _partialExtent;

                }
            }

            _previousPosition = _rb.position;
        }
    }
}