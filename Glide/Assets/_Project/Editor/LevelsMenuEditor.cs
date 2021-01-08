using Gisha.Glide.MainMenu.Levels;
using UnityEditor;
using UnityEngine;

namespace Gisha.Glide
{
    [CustomEditor(typeof (LevelsMenu))]
    public class LevelsMenuEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            var levelsMenu = (LevelsMenu)target;

            EditorGUILayout.BeginHorizontal();
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Reset Data"))
                levelsMenu.ResetLevelsData();
            GUI.backgroundColor = Color.blue;
            if (GUILayout.Button("Update UI"))
                levelsMenu.UpdateUI();
            EditorGUILayout.EndHorizontal();
        }
    }
}