using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.Glide.Game.Core
{
    public class LoadingManager : MonoBehaviour
    {
        #region Singleton
        private static LoadingManager Instance { get; set; }
        #endregion

        [Header("UI")]
        [SerializeField] private GameObject loadingCanvas = default;

        public static bool IsLoading { private set; get; }

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

        public static void AsyncLoad(string mainPath, params string[] additivePathes)
        {
            Instance.StartCoroutine(Instance.AsyncProgressCoroutine(mainPath, additivePathes));
        }

        private IEnumerator AsyncProgressCoroutine(string mainPath, params string[] additivePathes)
        {
            yield return null;
            loadingCanvas.SetActive(true);
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
            loadingCanvas.SetActive(false);
        }

        //private IEnumerator AsyncProgressCoroutine(LevelCoords coords)
        //{
        //    yield return null;
        //    loadingCanvas.SetActive(true);
        //    IsLoading = true;

        //    //--- DESTROY ROOT OBJECTS ON THE OLD SCENE ---//
        //    List<int> scenesToUnload = new List<int>();
        //    for (int i = 0; i < SceneManager.sceneCount; i++)
        //        scenesToUnload.Add(SceneManager.GetSceneAt(i).buildIndex);

        //    for (int i = 0; i < scenesToUnload.Count; i++)
        //    {
        //        var rootObjects = SceneManager.GetSceneByBuildIndex(scenesToUnload[i]).GetRootGameObjects();
        //        foreach (var obj in rootObjects)
        //            Destroy(obj);
        //    }

        //    //--- UNLOAD OLD SCENES ---//
        //    var unloadAsyncOperations = new List<AsyncOperation>();
        //    for (int i = 0; i < scenesToUnload.Count; i++)
        //        unloadAsyncOperations.Add(SceneManager.UnloadSceneAsync(scenesToUnload[i]));

        //    for (int i = unloadAsyncOperations.Count - 1; i > 0; i--)
        //    {
        //        while (!unloadAsyncOperations[i].isDone)
        //            yield return null;
        //    }

        //    //--- LOADING ---//
        //    var loadAsyncOperations = new List<AsyncOperation>();

        //    foreach (var sceneToLoad in scenesToLoad)
        //    {
        //        var ao = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        //        ao.allowSceneActivation = false;
        //        loadAsyncOperations.Add(ao);
        //    }

        //    //--- ACTIVATE NEW SCENES ---//
        //    for (int i = 0; i < loadAsyncOperations.Count; i++)
        //    {
        //        loadAsyncOperations[i].allowSceneActivation = true;

        //        while (!loadAsyncOperations[i].isDone)
        //            yield return null;
        //    }

        //    SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));

        //    IsLoading = false;
        //    loadingCanvas.SetActive(false);
        //}
    }
}