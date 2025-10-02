using Data.Sound;
using UnityEngine;

namespace Core.Scene
{
    public class PanelController : BaseController<PanelController>
    {
        [SerializeField] private UserInterfaceSoundData userInterfaceSoundData;
        [SerializeField] private AudioSource audioSource;
        
        public void HoverButton()
        {
            
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