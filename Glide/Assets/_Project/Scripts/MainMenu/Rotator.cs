using UnityEngine;

namespace Gisha.Glide.MainMenu
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float speed = default;

        Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            _transform.Rotate(Vector3.forward * speed);
        }
    }
}