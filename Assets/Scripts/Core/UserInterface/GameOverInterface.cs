using UnityEngine;

public class GameOverInterface : MonoBehaviour
{
    public void Start()
    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        GameController.Instance.OnGameOver += Show;
    }

    void Show()
    {
        gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        GameController.Instance.OnGameOver -= Show;
    }
}
