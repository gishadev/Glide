using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.Glide.Game.Core.SceneLoading
{
    public class LoadingManager : MonoBehaviour
    {
        #region Singleton
        private static LoadingManager Instance { get; set; }
        #endregion

        [Header("UI")]
        [SerializeField] private GameObject loadingCanvas = default;

        public static bool IsLoading { private set; get; }

        Animator _animator;

        private void Awake()
        {
            CreateInstance();

            _animator = GetComponent<Animator>();
        }

        private void CreateInstance()
        {
            DontDestroyOnLoad(gameObject);

            if (Instance == null)
                Instance = this;

            if (Instance != null && Instance != this)
                Destroy(gameObject);
        }

        public static void AsyncLoad(string mainPath, params string[] additivePathes) 
            => Instance.StartCoroutine(Instance.AsyncProgressCoroutine(mainPath, additivePathes));

        private IEnumerator AsyncProgressCoroutine(string mainPath, params string[] additivePathes)
        {
            yield return null;
            _animator.SetTrigger("Fade_In");
            IsLoading = true;

            //--- LOADING ---//
            var loadAsyncOperations = new List<AsyncOperation>();

            loadAsyncOperations.Add(SceneManager.LoadSceneAsync(mainPath, LoadSceneMode.Single));
            for (int i = 0; i < additivePathes.Length; i++)
                loadAsyncOperations.Add(SceneManager.LoadSceneAsync(additivePathes[i], LoadSceneMode.Additive));

            for (int i = 0; i < loadAsyncOperations.Count; i++)
            {
                while (loadAsyncOperations[i].progress < 0.9f)
                    yield return null;
            }

            yield return new WaitForSeconds(1f);

            IsLoading = false;
            _animator.SetTrigger("Fade_Out");
        }
    }
}