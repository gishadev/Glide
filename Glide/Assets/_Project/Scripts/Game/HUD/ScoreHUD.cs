using TMPro;
using UnityEngine;

namespace Gisha.Glide.Game.HUD
{
    public class ScoreHUD : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private TMP_Text scoreText = default;

        public void UpdateScoreText(int scoreToDisplay)
        {
            scoreText.text = scoreToDisplay.ToString();
        }
    }
}