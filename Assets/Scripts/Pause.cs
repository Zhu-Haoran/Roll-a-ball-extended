using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject resumeButton;
    private bool win;

    void Start()
    {
        win = false;
        pauseMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetAxis("Cancel/Menu") > 0 && !pauseMenu.activeInHierarchy)
        {
            PauseGame();
        }
    }

    public void GameWon()
    {
        PauseGame();
        resumeButton.SetActive(false);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        //enable the scripts again
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
