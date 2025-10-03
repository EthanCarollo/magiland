using System.Collections;
using Core.Controllers;
using Core.Scene;
using Data.Sound;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class GameOverInterface : MonoBehaviour
{
    [SerializeField] private  Slider slider;
    [SerializeField] private float timeBeforeLoadScene;
    private AudioSource audioSource;
    private float gameOverTime;
    private bool launchLoadingScene = false;
    
    public void Start()
    {
        GameController.Instance.OnGameOver += Show;

        audioSource = GetComponent<AudioSource>();

        gameObject.SetActive(false);
    }

    void Show()
    {
        gameObject.SetActive(true);
        audioSource.PlayOneShot(UserInterfaceSoundData.Instance.gameOverAudioClip);
        gameOverTime = Time.time + timeBeforeLoadScene;
        slider.value = 0;
        slider.maxValue = timeBeforeLoadScene;
    }
    
    void Update()
    {
        float value = timeBeforeLoadScene - (gameOverTime - Time.time);
        slider.value = value;
        if (value >= timeBeforeLoadScene && !launchLoadingScene)
        {
            launchLoadingScene = true;
            
            SceneTransitor.Instance.LoadScene(0, (() =>
            {
                Destroy(PlayerController.Instance?.transform.root.gameObject);
            }));
        }
    }

    void OnDestroy()
    {
        GameController.Instance.OnGameOver -= Show;
    }
}
