using System;
using UnityEngine;

namespace Core.Scene
{
    public class SceneTransitor : BaseController<SceneTransitor>
    {
        
        public delegate void LoadNewScene();
        public LoadNewScene OnLoadNewScene;
        
        public delegate void EndLoadNewScene();
        public EndLoadNewScene OnEndLoadNewScene;
    
        public GameObject loadingScreen;
    
        public void LoadScene(int sceneToLoad){
            OnLoadNewScene?.Invoke();
            var loadingScreenPrefab = GameObject.Instantiate(loadingScreen);
            loadingScreenPrefab.GetComponent<LoadingScreenController>()
                .StartToLoadScene(sceneToLoad, () =>
                {
                    OnEndLoadNewScene?.Invoke();
                });
        }
    
        public void LoadScene(int sceneToLoad, Action onEndCallback){
            OnLoadNewScene?.Invoke();
            var loadingScreenPrefab = GameObject.Instantiate(loadingScreen);
            loadingScreenPrefab.GetComponent<LoadingScreenController>()
                .StartToLoadScene(sceneToLoad, () =>
                {
                    onEndCallback?.Invoke();
                    OnEndLoadNewScene?.Invoke();
                });
        }
    }
}