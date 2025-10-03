using Core.Controllers;
using Data.Player;
using UnityEngine;

public class GameController : BaseController<GameController>
{
    public bool IsGamePaused = false;
    public delegate void GamePaused(bool isPaused);
    public event GamePaused OnGamePaused;

    public void Start()
    {
        if (InputController.Instance != null)
            InputController.Instance.OnPause += TogglePauseGame;
    }

    public void ResumeGame()
    {
        IsGamePaused = false;
        HandlePause();
    }

    public void PauseGame()
    {
        IsGamePaused = true;
        HandlePause();
    }

    public void TogglePauseGame()
    {
        IsGamePaused = !IsGamePaused;
        HandlePause();
    }

    void HandlePause()
    {
        Time.timeScale = IsGamePaused ? 0 : 1;
        OnGamePaused?.Invoke(IsGamePaused);
    }

}
