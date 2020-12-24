using System;
using UnityEngine;

namespace Gisha.Glide.Objects
{
    public class TriggerSignal : MonoBehaviour
    {
        [SerializeField] private bool singleEmit = false;

        public event Action<Collider> OnTriggerSignal;

        bool _isEmitted = false;

        private void OnTriggerEnter(Collider other)
        {
            if (singleEmit && _isEmitted)
                return;

            if (other.CompareTag("Airplane"))
            {
                OnTriggerSignal(other);
                _isEmitted = true;
            }
        }
    }
}