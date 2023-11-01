using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PauseMenu;

    public int newSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
    }
    public PlayerController PlayerScript;

    bool IsPaused;

    public void StartGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        IsPaused = false;
        newSceneIndex = 1;
        LoadNextLevel();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        IsPaused = true;
        PauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        IsPaused = false;
        PauseMenu.SetActive(false);
    }
    private void Update()
    {
        if (!IsPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        else if (IsPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
        if (PlayerScript.Lives <= 0) GameOver();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(2);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadSceneAsync(newSceneIndex);
    }

    public void LoadGameOver()
    {
        SceneManager.LoadSceneAsync(5);
    }
}
