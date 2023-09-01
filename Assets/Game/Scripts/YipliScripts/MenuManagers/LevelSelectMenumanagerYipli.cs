using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YipliFMDriverCommunication;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelectMenumanagerYipli : MonoBehaviour
{
    //required variables
    [SerializeField] Button[] allButtons;
    [SerializeField] Button backButton;

    private List<Button> menuButtons = new List<Button>();

    [SerializeField] PlayerStats ps;

    Button currentB;
    public int currentButtonIndex;

    const string LEFT = "left";
    const string RIGHT = "right";
    const string ENTER = "enter";

    const int totalLevels = 9; // change this number whenmore levels are added

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FindObjectOfType<Transition>().FadeInScene());

        foreach (Button b in allButtons)
        {
            b.GetComponent<Animator>().enabled = false;
        }

        SetClusterIDtoZero();

        SetAllowedLevels();
        manageCurrentButton();
    }

    private static void SetClusterIDtoZero()
    {
        try
        {
            Debug.Log("From level selection : Set cluster id to : 0");
            //YipliHelper.SetGameClusterId(0);
        }
        catch (Exception e)
        {
            Debug.Log("Level Selection cluster id 0 exception.");
            Debug.Log("Exception : " + e.Message);
        }
    }

    private void SetAllowedLevels()
    {
        int totalButtons;
        if (ps.GetCompletedLevels() >= totalLevels)
        {
            currentButtonIndex = totalLevels;
            totalButtons = totalLevels;
        }
        else
        {
            currentButtonIndex = ps.GetCompletedLevels();
            totalButtons = ps.GetCompletedLevels();
        }

        menuButtons.Add(backButton);

        for (int i = 0; i < totalButtons; i++)
        {
            menuButtons.Add(allButtons[i]);
        }
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
                if (menuButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>())
                {
                    menuButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
                }

                //menuButtons[i].GetComponent<Image>().color = Color.green;
                menuButtons[i].GetComponent<Animator>().enabled = true;
                currentB = menuButtons[i];
            }
            else
            {
                if (menuButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>())
                {
                    menuButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
                }

                //menuButtons[i].GetComponent<Image>().color = Color.white;
                menuButtons[i].GetComponent<Animator>().enabled = false;
                menuButtons[i].transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }


    private void MenuControlSystem()
    {
        string fmActionData = InitBLE.GetFMResponse();
        Debug.Log("Json Data from Fmdriver : " + fmActionData);

        if (fmActionData == null || fmActionData == "Make action") return;

        FmDriverResponseInfo singlePlayerResponse = JsonUtility.FromJson<FmDriverResponseInfo>(fmActionData);

        if (singlePlayerResponse == null) return;

        if (PlayerSession.Instance.currentYipliConfig.oldFMResponseCount != singlePlayerResponse.count)
        {
            //Debug.Log("FMResponse " + fmActionData);
            PlayerSession.Instance.currentYipliConfig.oldFMResponseCount = singlePlayerResponse.count;

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

    public void GotoMainMenu()
    {
        MatControlsStatManager.gameStateChanged(GameState.GAME_UI);
        //StartCoroutine(FindObjectOfType<Transition>().FadeOutScene("Main Menu"));

        SceneManager.LoadScene("Main Menu");
    }
}
