using Gisha.Glide.Game.HUD;

namespace Gisha.Glide.Game.Core
{
    public static class ScoreProcessor
    {
        public static int Score { get; private set; }

        static ScoreHUD _scoreHUD;

        public static void Initialize()
        {
            Score = 0;
            _scoreHUD = HUDManager.Instance.ScoreHUD;
        }

        public static void AddScore(int count)
        {
            if (count < 0)
                return;

            Score += count;
            _scoreHUD.UpdateScoreText(Score);
        }
    }
}