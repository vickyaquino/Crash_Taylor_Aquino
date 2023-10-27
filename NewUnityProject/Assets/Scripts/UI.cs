using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MainMenu, PauseMenu, GameOver;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }
    public PlayerController PlayerScript;

    bool IsPaused;

    public void StartGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        IsPaused = false;
        //come back to put in sceneloader 'loadscene' as a function
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
        if (PlayerScript.Lives <= 0) gameOver();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void gameOver()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        GameOver.SetActive(true);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(0);
    }
}
