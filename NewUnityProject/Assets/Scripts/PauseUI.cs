using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{

    public GameObject PauseMenu;

    bool IsPaused;

    public PlayerController PlayerScript;

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
    }

    public void goToStart()
    {
        SceneManager.LoadScene(0);
    }
}
