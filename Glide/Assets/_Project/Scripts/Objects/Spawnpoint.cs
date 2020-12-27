using UnityEngine;

namespace Gisha.Glide
{
    public class Spawnpoint : MonoBehaviour
    {
        #region Singleton
        public static Spawnpoint Instance { get; private set; }
        #endregion

        public Vector3 Position => transform.position;

        private void Awake() => Instance = this;
    }
}