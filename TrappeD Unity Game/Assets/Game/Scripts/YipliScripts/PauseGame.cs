using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject pauseArea;
    [SerializeField] GameObject InstructionCanvas;
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

    bool isPause = false;

    string currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;

        pauseArea.SetActive(false);
        InstructionCanvas.SetActive(true);

        currentButtonIndex = 0;
        manageCurrentButton();

        timeHistory1 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }

    private void Update()
    {
        GetMatKeyInputs();
        
        if (isPause)
        {
            MenuControlSystem();
        }
    }

    private void CalculateTime()
    {
        timer += Time.deltaTime;
    }

    public void pauseFunction()
    {
        isPause = true;

        if (currentScene != "Level_Tutorial")
        {
            PlayerSession.Instance.PauseSPSession();
        }

        SetClusterIDtoZero();

        InstructionCanvas.SetActive(false);
        pauseArea.SetActive(true);
        Time.timeScale = 0f;
    }

    private static void SetClusterIDtoZero()
    {
        try
        {
            Debug.Log("From pause function : Set cluster id to : 0");
            // set gameid to 0
            YipliHelper.SetGameClusterId(0);
        }
        catch (Exception e)
        {
            Debug.Log("Pause function cluster id 0 exception.");
            Debug.Log("Exception : " + e.Message);
        }
    }

    public void retryButton()
    {
        Time.timeScale = 1f;
        isPause = false;

        PlayerPrefs.DeleteKey("IS_CHKP_REACHED");
        PlayerPrefs.DeleteKey("CHKP_X");
        PlayerPrefs.DeleteKey("CHKP_Y");
        PlayerPrefs.DeleteKey("CHKP_Z");

        Debug.Log("Retry Function call");
        SetClusterIDtoOne();

        InstructionCanvas.SetActive(true);
        pauseArea.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private static void SetClusterIDtoOne()
    {
        try
        {
            Debug.Log("From retry or resume function : Set cluster id to : 1");
            YipliHelper.SetGameClusterId(1); // set current gameid
        }
        catch (Exception e)
        {
            Debug.Log("Retry or Resume function cluster id 1 exception.");
            Debug.Log("Exception : " + e.Message);
        }
    }

    public void resumeButton()
    {
        Time.timeScale = 1f;
        isPause = false;

        Debug.Log("Resume Function call");
        SetClusterIDtoOne();

        if (currentScene != "Level_Tutorial")
        {
            PlayerSession.Instance.ResumeSPSession();
        }

        InstructionCanvas.SetActive(true);
        pauseArea.SetActive(false);
    }

    private void manageCurrentButton()
    {
        for (int i = 0; i < menuButtons.Length; i++)
        {
            if (i == currentButtonIndex)
            {
                menuButtons[i].GetComponent<Image>().color = Color.green;
                menuButtons[i].GetComponent<Animator>().enabled = true;
                menuButtons[i].transform.GetChild(0).gameObject.SetActive(true);
                currentB = menuButtons[i];
            }
            else
            {
                menuButtons[i].GetComponent<Image>().color = Color.white;
                menuButtons[i].GetComponent<Animator>().enabled = false;
                menuButtons[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
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
