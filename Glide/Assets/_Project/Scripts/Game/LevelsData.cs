using UnityEngine;

namespace Gisha.Glide.Game
{

    [CreateAssetMenu(fileName = "LevelsData", menuName = "Scriptable Objects/Create Levels Data", order = 1)]
    public class LevelsData : ScriptableObject
    {
        public GalaxyData[] galaxies;
    }

    [System.Serializable]
    public struct GalaxyData
    {
        public string galaxyName;
        public string[] worldNames;
    }
}