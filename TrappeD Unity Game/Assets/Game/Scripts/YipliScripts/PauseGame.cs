using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject pauseArea;

    private void Start()
    {
        pauseArea.SetActive(false);
    }

    public void pauseButton ()
    {
        Time.timeScale = 0f;
        pauseArea.SetActive(true);
    }

    public void retryButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void resumeButton()
    {
        Time.timeScale = 1f;
        pauseArea.SetActive(false);
    }

    public void menuButtton ()
    {
        Time.timeScale = 1f;
    }
}
