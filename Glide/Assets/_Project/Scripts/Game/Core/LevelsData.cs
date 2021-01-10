using System.Collections.Generic;

namespace Gisha.Glide.Game.Core
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

        public string DebugText => $"[{GalaxyID},{WorldID},{LevelID}]";

        public LevelCoords(int galaxyID, int worldID, int levelID)
        {
            GalaxyID = galaxyID;
            WorldID = worldID;
            LevelID = levelID;
        }
    }
}