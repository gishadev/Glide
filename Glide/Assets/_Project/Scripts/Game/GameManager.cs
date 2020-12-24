using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.Glide.Game
{
    public static class GameManager
    {
        public static void ReloadScene() => SceneManager.LoadScene(0);
    }
}