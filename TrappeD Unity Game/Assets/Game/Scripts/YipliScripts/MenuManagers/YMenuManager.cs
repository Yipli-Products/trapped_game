using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YMenuManager : MonoBehaviour
{
    //required variables
    [SerializeField] Button[] menuButtons;

    Button currentB;

    int currentButtonIndex;

    const string LEFT = "left";
    const string RIGHT = "right";
    const string ENTER = "enter";
    const float waitTime = 0.75f;

    string FMResponseCount = "";

    float timer = 0;

    long timeHistory1 = 0;
    long timeHistory2 = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetClusterIDtoZero();

        currentButtonIndex = 0;
        manageCurrentButton();

        timeHistory1 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }

    private static void SetClusterIDtoZero()
    {
        try
        {
            Debug.Log("From main menu : Set cluster id to : 0");
            PlayerSession.Instance.SetGameClusterId(0);
        }
        catch (Exception e)
        {
            Debug.Log("Main Menu Manager cluster id 0 exception.");
            Debug.Log("Exception : " + e.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetMatKeyInputs();
        TimeControlSystem();

        //CalculateTime();
    }

    private void TimeControlSystem()
    {
        timeHistory2 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        Debug.Log(timeHistory2 + "-" + timeHistory1 + "=" + (timeHistory2 - timeHistory1));

        if ((timeHistory2 - timeHistory1) > 1000)
        {
            MenuControlSystem();

            timeHistory1 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            Debug.Log("Time changed : " + timeHistory1);
        }
    }

    private void CalculateTime()
    {
        timer += Time.deltaTime;
    }

    private void GetMatKeyInputs()
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
        //#if UNITY_ANDROID
        //string FMResponse = PlayerMovement.PluginClass.CallStatic<string>("_getFMResponse");

        string FMResponse = InitBLE.PluginClass.CallStatic<string>("_getFMResponse");
        Debug.Log("UNITY FMResponse: " + FMResponse);

        string[] FMTokens = FMResponse.Split('.');
        Debug.Log("UNITY FMTokens: " + FMTokens[0]);

        if (!FMTokens[0].Equals(FMResponseCount))
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
