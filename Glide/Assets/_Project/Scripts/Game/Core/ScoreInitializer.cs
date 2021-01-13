using UnityEngine;

namespace Gisha.Glide.Game.Core
{
    public class ScoreInitializer : MonoBehaviour
    {
        private void Start()
        {
            ScoreProcessor.Initialize();
        }
    }
}