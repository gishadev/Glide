using System.Collections.Generic;
using UnityEngine;

namespace Gisha.Glide.Game
{
    [System.Serializable]
    public class LevelsData
    {
        public Dictionary<LevelCoords, LevelData> allLevels;

        public LevelsData(Dictionary<LevelCoords, LevelData> allLevels)
        {
            this.allLevels = allLevels;
        }
    }


    [System.Serializable]
    public class LevelData
    {
        public LevelState LevelState { get; private set; }

        public LevelData(int levelState)
        {
            LevelState = (LevelState)levelState;
        }

        public void SetLevelState(LevelState state) => LevelState = state;
    }

    public enum LevelState
    {
        Passed,
        Next,
        Hidden,
        Nonexistent
    }

    public struct LevelCoords
    {
        public int GalaxyID { private set; get; }
        public int WorldID { private set; get; }
        public int LevelID { private set; get; }

        public LevelCoords(int galaxyID, int worldID, int levelID)
        {
            this.GalaxyID = galaxyID;
            this.WorldID = worldID;
            this.LevelID = levelID;
        }
    }
}