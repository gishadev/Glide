using Gisha.Glide.Game.Core;
using TMPro;
using UnityEngine;

namespace Gisha.Glide.Game.HUD
{
    public class ScoreHUD : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private TMP_Text scoreText = default;

        private void Start()
        {
            ScoreProcessor.Initialize();
        }

        public void UpdateScoreText(int scoreToDisplay)
        {
            scoreText.text = scoreToDisplay.ToString();
        }
    }
}