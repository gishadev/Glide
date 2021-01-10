using Gisha.Glide.Game.Core;
using TMPro;
using UnityEngine;

namespace Gisha.Glide.MainMenu.Levels
{
    public class LevelInfoBox : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private GameObject body = default;
        [SerializeField] private Vector2 popupOffset = default;
        [SerializeField] private LevelsMap levelsMap = default;

        [Header("Content")]
        [SerializeField] private TMP_Text levelText = default;
        [SerializeField] private TMP_Text worldText = default;
        [SerializeField] private TMP_Text bestScoreText = default;


        private void Awake()
        {
            if (body == null) Debug.LogError("body is not assigned!");
            if (popupOffset == null) Debug.LogError("popupOffset is not assigned!");
            if (levelsMap == null) Debug.LogError("levelsMap is not assigned!");
            if (levelText == null || worldText == null || bestScoreText == null) Debug.LogError("Content is not assigned!");
        }

        public void Popup(Vector2 screenPosition, LevelCoords coords)
        {
            transform.position = screenPosition + popupOffset;

            levelText.text = $"Level {coords.LevelID + 1}";
            worldText.text = levelsMap.galaxies[coords.GalaxyID].worldNames[coords.WorldID];
            bestScoreText.text = "228";

            body.SetActive(true);
        }

        public void Hide()
        {
            body.SetActive(false);
        }
    }
}