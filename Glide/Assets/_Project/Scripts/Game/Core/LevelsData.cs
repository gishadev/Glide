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
        public int BestScore { get; private set; }

        public LevelData(int levelState, int bestScore)
        {
            LevelState = (LevelState)levelState;
            BestScore = bestScore;
        }

        public void SetLevelState(LevelState state) => LevelState = state;
        public void SetBestScore(int bestScore) => BestScore = bestScore;
    }

    public enum LevelState
    {
        Passed,
        Next,
        Hidden,
        Nonexistent
    }
}