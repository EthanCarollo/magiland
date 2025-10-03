using Core.Scene;
using Data.Player;
using Data.Sound;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PauseInterface : MonoBehaviour
{
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private Slider controllerSensitivitySlider;
    private AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        GameController.Instance.OnGamePaused += Display;

        PlayerSettingsData playerSettingsData = PlayerSettingsData.Instance;
        mouseSensitivitySlider.value = playerSettingsData.mouseSensitivity;
        controllerSensitivitySlider.value = playerSettingsData.controllerSensitivity;

        gameObject.SetActive(false);
    }

    void Display(bool isPaused)
    {
        gameObject.SetActive(isPaused);

        if (isPaused)
        {
            audioSource.PlayOneShot(UserInterfaceSoundData.Instance.openPopupAudioClip);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void UpdateMouseSensitivity(float value)
    {
        PlayerSettingsData.Instance.mouseSensitivity = value;
    }

    public void UpdateControllerSensitivity(float value)
    {
        PlayerSettingsData.Instance.controllerSensitivity = value;
    }

    public void ResumeGame()
    {
        GameController.Instance.ResumeGame();
    }

    public void ReturnToMainMenu()
    {
        if (SceneTransitor.Instance != null)
        {
            audioSource.PlayOneShot(UserInterfaceSoundData.Instance.clickButtonAudioClip);
            GameController.Instance.ResumeGame();
            SceneTransitor.Instance.LoadScene(0);
        }
        else
        {
            Debug.Log("SceneTransitor is null");
        }
    }

    void OnDestroy()
    {
        GameController.Instance.OnGamePaused -= Display;
    }
}
