using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Gisha.Glide.Game.HUD
{
    public class CanvasFader : MonoBehaviour
    {
        #region Singleton
        private static CanvasFader Instance { get; set; }
        #endregion

        [Header("UI")]
        [SerializeField] private Image fadeImg = default;

        public static event Action FullFaded;

        private void Awake()
        {
            CreateInstance();
        }

        private void CreateInstance()
        {
            DontDestroyOnLoad(gameObject);

            if (Instance == null)
                Instance = this;

            if (Instance != null && Instance != this)
                Destroy(gameObject);
        }

        public static void FadeIn()
        {
            Instance.StopAllCoroutines();
            Instance.StartCoroutine(Instance.FadeInCoroutine());
        }

        public static void FadeOut()
        {
            Instance.StopAllCoroutines();
            Instance.StartCoroutine(Instance.FadeOutCoroutine());
        }

        #region Coroutines
        private IEnumerator FadeInCoroutine()
        {
            var alpha = 0f;
            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, alpha);
            while (alpha < 1f)
            {
                alpha += Time.deltaTime * 4f;
                fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, alpha);
                yield return null;
            }

            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, 1f);
            FullFaded();
        }

        private IEnumerator FadeOutCoroutine()
        {
            var alpha = 1f;
            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, alpha);
            while (alpha > 0f)
            {
                alpha -= Time.deltaTime * 4f;
                fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, alpha);
                yield return null;
            }

            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, 0f);
        }
        #endregion
    }
}