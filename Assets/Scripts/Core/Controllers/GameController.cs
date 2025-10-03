using Core.Controllers;
using Data.Player;
using NUnit.Framework;
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
        if (IsGameOver) return;
        IsGamePaused = false;
        HandleTimePause();
        OnGamePaused?.Invoke(IsGamePaused);
    }

    public void PauseGame()
    {
        if (IsGameOver) return;
        IsGamePaused = true;
        HandleTimePause();
        OnGamePaused?.Invoke(IsGamePaused);
    }

    public void TogglePauseGame()
    {
        if (IsGameOver) return;
        IsGamePaused = !IsGamePaused;
        HandleTimePause();
        OnGamePaused?.Invoke(IsGamePaused);
    }

    void HandleTimePause()
    {
        Time.timeScale = IsGamePaused ? 0 : 1;
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
