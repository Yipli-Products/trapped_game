using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YipliFMDriverCommunication;

public class LevelSelectMenumanagerYipli : MonoBehaviour
{
    //required variables
    [SerializeField] Button[] allButtons;
    [SerializeField] Button backButton;

    private List<Button> menuButtons = new List<Button>();

    [SerializeField] PlayerStats ps;

    Button currentB;
    int currentButtonIndex;

    const string LEFT = "left";
    const string RIGHT = "right";
    const string ENTER = "enter";

    int FMResponseCount = -1;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteKey("IS_CHKP_REACHED");
        PlayerPrefs.DeleteKey("CHKP_X");
        PlayerPrefs.DeleteKey("CHKP_Y");
        PlayerPrefs.DeleteKey("CHKP_Z");

        SetClusterIDtoZero();

        SetAllowedLevels();
        manageCurrentButton();
    }

    private static void SetClusterIDtoZero()
    {
        try
        {
            Debug.Log("From level selection : Set cluster id to : 0");
            YipliHelper.SetGameClusterId(0);
        }
        catch (Exception e)
        {
            Debug.Log("Level Selection cluster id 0 exception.");
            Debug.Log("Exception : " + e.Message);
        }
    }

    private void SetAllowedLevels()
    {
        for (int i = 0; i < ps.GetCompletedLevels() + 1; i++)
        {
            menuButtons.Add(allButtons[i]);
        }
        menuButtons.Add(backButton);
        currentButtonIndex = ps.GetCompletedLevels();
    }

    // Update is called once per frame
    void Update()
    {
        GetMatKeyInputs();
        MenuControlSystem();
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
        for (int i = 0; i < menuButtons.Count; i++)
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
        string fmActionData = InitBLE.PluginClass.CallStatic<string>("_getFMResponse");
        Debug.Log("Json Data from Fmdriver : " + fmActionData);

        FmDriverResponseInfo singlePlayerResponse = JsonUtility.FromJson<FmDriverResponseInfo>(fmActionData);

        if (singlePlayerResponse == null) return;

        if (FMResponseCount != singlePlayerResponse.count)
        {
            Debug.Log("FMResponse " + fmActionData);
            FMResponseCount = singlePlayerResponse.count;

            YipliUtils.PlayerActions providedAction = ActionAndGameInfoManager.GetActionEnumFromActionID(singlePlayerResponse.playerdata[0].fmresponse.action_id);

            switch (providedAction)
            {
                case YipliUtils.PlayerActions.LEFT:
                    ProcessMatInputs(LEFT);
                    break;

                case YipliUtils.PlayerActions.RIGHT:
                    ProcessMatInputs(RIGHT);
                    break;

                case YipliUtils.PlayerActions.ENTER:
                    ProcessMatInputs(ENTER);
                    break;
            }
        }
    }

    private int GetNextButton()
    {
        if ((currentButtonIndex + 1) == menuButtons.Count)
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
            return menuButtons.Count - 1;
        }
        else
        {
            return currentButtonIndex - 1;
        }
    }

    public void ProcessMatInputs(string matInput)
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
