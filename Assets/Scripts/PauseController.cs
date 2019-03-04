using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject resumeButton;
    public GameObject NextLevelButton;
    public GameObject RestartButton;
    public int nextLevel = 0;

    private bool m_isWin;
    private bool m_isMenuInputDown;

    void Start()
    {
        m_isMenuInputDown = false;
        m_isWin = false;
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (!m_isWin && Input.GetAxisRaw("Menu") != 0)
        {
            if (m_isMenuInputDown == false)
            {
                if (pausePanel.activeInHierarchy)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
                m_isMenuInputDown = true;
            }
        }
        else
        {
            m_isMenuInputDown = false;
        }
    }

    public void GameWon()
    {
        m_isWin = true;
        PauseGame();

        resumeButton.SetActive(false);
        if (nextLevel != 0)
        {
            NextLevelButton.SetActive(true);
            EventSystem.current.SetSelectedGameObject(NextLevelButton);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(RestartButton);
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        if (!m_isWin)
        {
            EventSystem.current.SetSelectedGameObject(resumeButton);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level " + nextLevel.ToString());
    }

    public void SelectLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level Selection");
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
