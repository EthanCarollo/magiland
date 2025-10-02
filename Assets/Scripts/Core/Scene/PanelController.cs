using UnityEngine;

namespace Core.Scene
{
    public class PanelController : MonoBehaviour
    {
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