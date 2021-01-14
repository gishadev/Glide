using UnityEngine;

namespace Gisha.Glide.Game.Objects
{
    public class Turbine : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private float angularSpeed = default;

        Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            _transform.Rotate(Vector3.forward * angularSpeed * Time.deltaTime);
        }

    }
}