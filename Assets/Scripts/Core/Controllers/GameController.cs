using System;
using Core.Controllers;
using Core.Scene;
using Data.Player;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        
        SceneTransitor.Instance.OnLoadNewScene += OnNewScene;
        SceneTransitor.Instance.OnEndLoadNewScene += OnEndLoadNewScene;
    }

    public void OnDisable()
    {
        if(SceneTransitor.Instance != null)
            SceneTransitor.Instance.OnLoadNewScene -= OnNewScene;
    }

    public void OnEndLoadNewScene() {
        if (FindAnyObjectByType<PlayerController>() != null)
            FindAnyObjectByType<PlayerController>().LifeChanged += OnLifeChanged;
    }

    public void OnNewScene()
    {
        IsGameOver = false;
        IsGamePaused = false;
        HandleTimePause();
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
        Debug.Log("Handle time pause called");
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Time.timeScale = 1;
            return;
        }

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
