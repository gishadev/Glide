using Gisha.Glide.Game;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Gisha.Glide
{
    public class SceneQuickChange
    {
        [MenuItem("Tools/Quick Change: Main Menu")]
        static void ChangeToMainMenu() 
            => EditorSceneManager.OpenScene("Assets/" + PathBuilder.GetPathToMainScene("MainMenu") + ".unity", OpenSceneMode.Single);

        [MenuItem("Tools/Quick Change: Game")]
        static void ChangeToGame()
        {
            EditorSceneManager.OpenScene("Assets/" + PathBuilder.GetPathToMainScene("Game") + ".unity");
            EditorSceneManager.OpenScene("Assets/" + PathBuilder.GetScenePathFromCoords(new LevelCoords(0, 0, 0)) + ".unity", OpenSceneMode.Additive);
        }
    }
}