using UnityEngine;

namespace Gisha.Glide.Game.Objects
{
    public class LookAtCamera : MonoBehaviour
    {
        Transform _transform;
        Camera _cam;

        private void Awake()
        {
            _transform = transform;
            _cam = Camera.main;
        }

        private void Update()
        {
            _transform.LookAt(_cam.transform);
        }
    }
}