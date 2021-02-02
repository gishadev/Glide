using UnityEngine;

namespace Gisha.Glide.Game.Objects
{
    public class Accumulator : MonoBehaviour
    {
        bool _isActive = true;

        public void Destroy()
        {
            _isActive = false;
        }

        private void OnDrawGizmos()
        {
            if (_isActive)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 5f);
        }
    }
}