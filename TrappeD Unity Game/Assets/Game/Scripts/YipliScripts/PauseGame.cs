using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject pauseArea;
    [SerializeField] Button[] menuButtons;

    Button currentB;
    int currentButtonIndex;

    LoadLevelByName llbn;

    bool leftPressed = false;
    bool rightPressed = false;
    bool EnterPressed = false;

    string FMResponseCount = "";

    float timer = 0;

    private void Start()
    {
        llbn = FindObjectOfType<LoadLevelByName>();
        pauseArea.SetActive(false);

        currentButtonIndex = 0;
        manageCurrentButton();
    }

    private void Update()
    {
        MenuControlSystem();

        if (pauseArea.activeSelf)
        {
            GetMatKeyInputs();
        }

        CalculateTime();
    }

    private void CalculateTime()
    {
        timer += Time.deltaTime;
    }

    public void pauseButton ()
    {
        PlayerSession.Instance.PauseSPSession();
        Time.timeScale = 0f;

        // set gameid to 0
        PlayerSession.Instance.SetGameClusterId(0);
        pauseArea.SetActive(true);
    }

    public void retryButton()
    {
        Time.timeScale = 1f;
        PlayerSession.Instance.SetGameClusterId(1); // set current gameid
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void resumeButton()
    {

        Time.timeScale = 1f;
        PlayerSession.Instance.SetGameClusterId(1); // set current gameid
        PlayerSession.Instance.ResumeSPSession();
        pauseArea.SetActive(false);
    }

    public void menuButtton ()
    {
        Time.timeScale = 1f;
    }

    private void GetMatKeyInputs()
    {
        // left to right resume, menu, retry

        if (Input.GetKeyDown(KeyCode.LeftArrow) || leftPressed)
        {
            currentButtonIndex = GetPreviousButton();
            manageCurrentButton();
            leftPressed = false;
        }

        // left to right resume, menu, retry
        if (Input.GetKeyDown(KeyCode.RightArrow) || rightPressed)
        {
            currentButtonIndex = GetNextButton();
            manageCurrentButton();
            rightPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) || EnterPressed)
        {
            currentB.GetComponent<Button>().onClick.Invoke();
            EnterPressed = false;
        }
    }

    private void manageCurrentButton()
    {
        for (int i = 0; i < menuButtons.Length; i++)
        {
            if (i == currentButtonIndex)
            {
                menuButtons[i].GetComponent<Image>().color = Color.green;
                currentB = menuButtons[i];
            }
            else
            {
                menuButtons[i].GetComponent<Image>().color = Color.white;
            }
        }
    }

    private void MenuControlSystem()
    {
        if (timer > 1f)
        {
            //#if UNITY_ANDROID
            //string FMResponse = PlayerMovement.PluginClass.CallStatic<string>("_getFMResponse");
            string FMResponse = InitBLE.PluginClass.CallStatic<string>("_getFMResponse");
            Debug.Log("UNITY FMResponse: " + FMResponse);

            string[] FMTokens = FMResponse.Split('.');
            Debug.Log("UNITY FMTokens: " + FMTokens[0]);

            if (!FMTokens[0].Equals(FMResponseCount))
            {
                FMResponseCount = FMTokens[0];
                if (FMTokens[1] == "Pause")
                {
                    pauseButton();
                }
                else if (FMTokens[1] == "Left")
                {
                    leftPressed = true;
                }
                else if (FMTokens[1] == "Right")
                {
                    rightPressed = true;
                }
                else if (FMTokens[1] == "Enter")
                {
                    EnterPressed = true;
                }
            }

            timer = 0;
        }
    }

    private int GetNextButton()
    {
        if ((currentButtonIndex + 1) == menuButtons.Length)
        {
            return 0;
        }
        else
        {
            return currentButtonIndex + 1;
        }
    }

    private int GetPreviousButton()
    {
        if (currentButtonIndex == 0)
        {
            return menuButtons.Length - 1;
        }
        else
        {
            return currentButtonIndex - 1;
        }
    }
}
