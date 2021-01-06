using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Gisha.Glide.Game
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "Scriptable Objects/Create Levels Data", order = 1)]
    public class LevelsData : ScriptableObject
    {
        public GalaxyData[] galaxies;
        public Dictionary<LevelCoords, LevelData> allLevels;
    }

    public enum LevelState
    {
        Passed,
        Next,
        Hidden,
        Nonexistent
    }

    [System.Serializable]
    public struct GalaxyData
    {
        public string galaxyName;
        public string[] worldNames;
    }

    [System.Serializable]
    public struct LevelData
    {
        public SceneAsset levelScene;
        public LevelState LevelState { get => _levelState; private set => _levelState = value; }

         LevelState _levelState;

        public LevelData(SceneAsset levelScene, LevelState levelState)
        {
            this.levelScene = levelScene;
            _levelState = levelState;
        }

        public void SetLevelState(LevelState state)
        {
            LevelState = state;
        }
    }

}