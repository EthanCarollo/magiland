using Data.Sound;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Core.Scene
{
    public class PanelController : BaseController<PanelController>
    {
        [SerializeField] private UserInterfaceSoundData userInterfaceSoundData;
        [SerializeField] private AudioSource audioSource;
        
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button exitGameButton;

        private void Awake()
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
            audioSource.PlayOneShot(userInterfaceSoundData.hoverButtonAudioClip);
        }

        public void ClickButton()
        {
            audioSource.PlayOneShot(userInterfaceSoundData.clickButtonAudioClip);
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