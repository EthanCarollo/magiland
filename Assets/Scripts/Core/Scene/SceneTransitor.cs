using System;
using UnityEngine;

namespace Core.Scene
{
    public class SceneTransitor : MonoBehaviour
    {
        public static SceneTransitor Instance;
    
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
            var loadingScreenPrefab = GameObject.Instantiate(loadingScreen);
            loadingScreenPrefab.GetComponent<LoadingScreenController>()
                .StartToLoadScene(sceneToLoad);
        }
    
        public void LoadScene(int sceneToLoad, Action onEndCallback){
            var loadingScreenPrefab = GameObject.Instantiate(loadingScreen);
            loadingScreenPrefab.GetComponent<LoadingScreenController>()
                .StartToLoadScene(sceneToLoad, onEndCallback);
        }
    }
}