using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenumanagerYipli : MonoBehaviour
{
    //required variables
    [SerializeField] Button[] menuButtons;
    [SerializeField] PlayerStats ps;
    [SerializeField] Button back;

    private List<Button> allowedLevels;

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

        allowedLevels = new List<Button>();

        SetAllowedLevels();
        SetCurrentButtonIndex();
        manageCurrentButton();

        timeHistory1 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }

    private static void SetClusterIDtoZero()
    {
        try
        {
            Debug.Log("From level selection : Set cluster id to : 0");
            PlayerSession.Instance.SetGameClusterId(0);
        }
        catch (Exception e)
        {
            Debug.Log("Level Selection cluster id 0 exception.");
            Debug.Log("Exception : " + e.Message);
        }
    }

    private void SetCurrentButtonIndex()
    {
        if (ps.GetCompletedLevels() >= 10)
        {
            currentButtonIndex = 9;
        }
        else
        {
            currentButtonIndex = ps.GetCompletedLevels() + 1;
        }
    }

    private void SetAllowedLevels()
    {
        int allowedButtons = ps.GetCompletedLevels();

        if (allowedButtons > 9)
        {
            allowedButtons = 10;
        }
        else
        {
            allowedButtons += 1;
        }

        allowedLevels.Add(back);
        
        for (int i = 0; i < allowedButtons - 1; i++)
        {
            allowedLevels.Add(menuButtons[i]);
        }
        print("allowed levels : " + allowedLevels.Count);
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
        for (int i = 0; i < allowedLevels.Count; i++)
        {
            if (i == currentButtonIndex)
            {
                allowedLevels[i].GetComponent<Image>().color = Color.green;
                currentB = allowedLevels[i];
            }
            else
            {
                allowedLevels[i].GetComponent<Image>().color = Color.white;
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
