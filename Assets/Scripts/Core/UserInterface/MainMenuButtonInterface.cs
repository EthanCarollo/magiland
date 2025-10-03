using Data.Sound;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Core.Scene
{
    public class MainMenuButtonInterface : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button exitGameButton;

        private void Start()
        {
            if (startGameButton != null)
            {
                startGameButton.onClick.AddListener(OnStartGameButtonClicked);
                startGameButton.onClick.AddListener(ClickButton);
                AddHoverSound(startGameButton);
            }

            if (exitGameButton != null)
            {
                exitGameButton.onClick.AddListener(OnExitGameButtonClicked);
                exitGameButton.onClick.AddListener(ClickButton);
                AddHoverSound(exitGameButton);
            }
        }

        private void AddHoverSound(Button button)
        {
            var trigger = button.gameObject.AddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter
            };
            entry.callback.AddListener((eventData) => HoverButton());
            trigger.triggers.Add(entry);
        }

        public void HoverButton()
        {
            audioSource.PlayOneShot(UserInterfaceSoundData.Instance.hoverButtonAudioClip);
        }

        public void ClickButton()
        {
            audioSource.PlayOneShot(UserInterfaceSoundData.Instance.clickButtonAudioClip);
        }

        private void OnStartGameButtonClicked()
        {
            StartGame();
        }

        private void OnExitGameButtonClicked()
        {
            QuitGame();
        }

        public void StartGame()
        {
            SceneTransitor.Instance.LoadScene(1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}