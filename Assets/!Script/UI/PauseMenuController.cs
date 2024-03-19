using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject optionUI;
    public GameObject pauseMenuPanel;
    public bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            if(optionUI)
            {
                optionUI.SetActive(false);
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            pauseMenuPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenuPanel.SetActive(false);
        }
    }
    public void OnBackOptionBtnClick()
    {
        optionUI.SetActive(false);
    }

    public void Resume()
    {
        TogglePause();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void OnOptionBtnClick()
    {
        optionUI.SetActive(true);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
