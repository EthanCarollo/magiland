using Core.Controllers;
using Data.Player;
using UnityEngine;

public class GameController : BaseController<GameController>
{
    public bool IsGamePaused = false;
    public delegate void GamePaused(bool isPaused);
    public event GamePaused OnGamePaused;
    public bool IsGameOver = false;
    public delegate void Gameover();
    public event Gameover OnGameOver;

    public void Start()
    {
        if (PlayerController.Instance != null)
            PlayerController.Instance.LifeChanged += OnLifeChanged;

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

    void OnLifeChanged(int life)
    {
        if (life <= 0) GameOver();
    }

    void GameOver()
    {
        IsGameOver = true;
        OnGameOver?.Invoke();
    }
}
