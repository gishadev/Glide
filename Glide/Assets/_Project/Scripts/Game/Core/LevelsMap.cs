using UnityEngine;

namespace Gisha.Glide.Game.Core
{
    [CreateAssetMenu(fileName = "LevelsMap", menuName = "Scriptable Objects/Create Levels Map", order = 1)]
    public class LevelsMap : ScriptableObject
    {
        public GalaxyData[] galaxies;
    }

    [System.Serializable]
    public struct GalaxyData
    {
        public string galaxyName;
        public string[] worldNames;
        public int[] levelsCount;
    }
}