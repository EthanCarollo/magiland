using System;
using UnityEngine;

namespace Core.Scene
{
    public class SceneTransitor : MonoBehaviour
    {
        public static SceneTransitor Instance;
        
        public delegate void LoadNewScene();
        public LoadNewScene OnLoadNewScene;
    
        public GameObject loadingScreen;

        public void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            Instance = this;
            DontDestroyOnLoad(this);
        }
    
        public void LoadScene(int sceneToLoad){
            OnLoadNewScene?.Invoke();
            var loadingScreenPrefab = GameObject.Instantiate(loadingScreen);
            loadingScreenPrefab.GetComponent<LoadingScreenController>()
                .StartToLoadScene(sceneToLoad);
        }
    
        public void LoadScene(int sceneToLoad, Action onEndCallback){
            OnLoadNewScene?.Invoke();
            var loadingScreenPrefab = GameObject.Instantiate(loadingScreen);
            loadingScreenPrefab.GetComponent<LoadingScreenController>()
                .StartToLoadScene(sceneToLoad, onEndCallback);
        }
    }
}