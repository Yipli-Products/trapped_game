using Boo.Lang;
using System;
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
    }

    // Update is called once per frame
    void Update()
    {
        GetMatKeyInputs();
        //MenuControlSystem();
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
        //#if UNITY_ANDROID
        //string FMResponse = PlayerMovement.PluginClass.CallStatic<string>("_getFMResponse");

        string fmActionData = InitBLE.PluginClass.CallStatic<string>("_getFMResponse");

        FmDriverResponseInfo singlePlayerResponse = JsonUtility.FromJson<FmDriverResponseInfo>(fmActionData);

        if (FMResponseCount != singlePlayerResponse.count)
        {
            Debug.Log("FMResponse " + fmActionData);
            FMResponseCount = singlePlayerResponse.count;

            if (singlePlayerResponse.playerdata[0].fmresponse.action_id.Equals(ActionAndGameInfoManager.getActionIDFromActionName(YipliUtils.PlayerActions.LEFT)))
            {
                ProcessMatInputs(LEFT);
            }
            else if (singlePlayerResponse.playerdata[0].fmresponse.action_id.Equals(ActionAndGameInfoManager.getActionIDFromActionName(YipliUtils.PlayerActions.RIGHT)))
            {
                ProcessMatInputs(RIGHT);
            }
            else if (singlePlayerResponse.playerdata[0].fmresponse.action_id.Equals(ActionAndGameInfoManager.getActionIDFromActionName(YipliUtils.PlayerActions.ENTER)))
            {
                ProcessMatInputs(ENTER);
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
