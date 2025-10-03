using System;
using UnityEngine;

namespace Core.Scene
{
    public class SceneTransitor : BaseController<SceneTransitor>
    {
        
        public delegate void LoadNewScene();
        public LoadNewScene OnLoadNewScene;
    
        public GameObject loadingScreen;
    
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