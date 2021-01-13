using Gisha.Glide.Game.HUD;

namespace Gisha.Glide.Game.Core
{
    public static class ScoreProcessor
    {
        public static int Score { get; private set; }

        static bool _isInitialized = false;
        static ScoreHUD _scoreHUD;

        private static void Initialize()
        {
            _scoreHUD = HUDManager.Instance.ScoreHUD;
            _isInitialized = true;
        }

        public static void AddScore(int count)
        {
            if (!_isInitialized)
                Initialize();

            if (count < 0)
                return;

            Score += count;
            _scoreHUD.UpdateScoreText(Score);
        }
    }
}