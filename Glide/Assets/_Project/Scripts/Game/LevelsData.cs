using System.Collections.Generic;
using UnityEngine;

namespace Gisha.Glide.Game
{

    [CreateAssetMenu(fileName = "LevelsData", menuName = "Scriptable Objects/Create Levels Data", order = 1)]
    public class LevelsData : ScriptableObject
    {
        public GalaxyData[] galaxies;
        public List<LevelCoords> levelsCoords = new List<LevelCoords>();
    }

    [System.Serializable]
    public struct GalaxyData
    {
        public string galaxyName;
        public WorldData[] worlds;
    }

    [System.Serializable]
    public struct WorldData
    {
        public string worldName;
        public UnityEditor.SceneAsset[] levelScenes;
    }
}