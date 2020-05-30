using System;
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

    const string LEFT = "left";
    const string RIGHT = "right";
    const string ENTER = "enter";

    string FMResponseCount = "";

    float timer = 0;

    private void Start()
    {
        pauseArea.SetActive(false);

        currentButtonIndex = 0;
        manageCurrentButton();
    }

    private void Update()
    {
        MenuControlSystem();
        CalculateTime();
    }

    private void CalculateTime()
    {
        timer += Time.deltaTime;
    }

    public void pauseButton ()
    {
        print("From pause function : Set cluster id to : 0");
        // set gameid to 0
        //PlayerSession.Instance.SetGameClusterId(0);

        PlayerSession.Instance.PauseSPSession();
        Time.timeScale = 0f;
        pauseArea.SetActive(true);
    }

    public void retryButton()
    {
        print("From retry function : Set cluster id to : 1");
        //PlayerSession.Instance.SetGameClusterId(1); // set current gameid

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void resumeButton()
    {
        print("From resume function : Set cluster id to : 1");
        //PlayerSession.Instance.SetGameClusterId(1); // set current gameid

        PlayerSession.Instance.ResumeSPSession();

        Time.timeScale = 1f;
        pauseArea.SetActive(false);
    }

    public void menuButtton ()
    {
        Time.timeScale = 1f;
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

    /*private void GetMatKeyInputs()
    {
        // left to right play, changeplayer, gotoyipli, exit
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ProcessMatInputs("left");
        }

        // left to right play, changeplayer, gotoyipli, exit
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ProcessMatInputs("right");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ProcessMatInputs("enter");
        }
    }*/

    private void MenuControlSystem()
    {
        // timer conditions to map with 1s time
        if (timer > 0.5f)
        {
            //#if UNITY_ANDROID
            //string FMResponse = PlayerMovement.PluginClass.CallStatic<string>("_getFMResponse");

            string FMResponse = InitBLE.PluginClass.CallStatic<string>("_getFMResponse");
            Debug.Log("UNITY FMResponse: " + FMResponse);

            string[] FMTokens = FMResponse.Split('.');
            Debug.Log("UNITY FMTokens: " + FMTokens[0]);

            if (FMTokens.Length > 1 && !FMTokens[0].Equals(FMResponseCount))
            {
                FMResponseCount = FMTokens[0];
                if (FMTokens[1].Equals("Left", StringComparison.OrdinalIgnoreCase))
                {
                    ProcessMatInputs(LEFT);
                }
                else if (FMTokens[1].Equals("Right", StringComparison.OrdinalIgnoreCase))
                {
                    ProcessMatInputs(RIGHT);
                }
                else if (FMTokens[1].Equals("Enter", StringComparison.OrdinalIgnoreCase))
                {
                    ProcessMatInputs(ENTER);
                }
            }

            timer = 0f;
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

    private void ProcessMatInputs(string matInput)
    {
        switch (matInput)
        {
            case LEFT:
                currentButtonIndex = GetPreviousButton();
                manageCurrentButton();
                break;

            case RIGHT:
                currentButtonIndex = GetNextButton();
                manageCurrentButton();
                break;

            case ENTER:
                currentB.GetComponent<Button>().onClick.Invoke();
                break;

            default:
                Debug.Log("Wrong Input");
                break;
        }
    }
}
