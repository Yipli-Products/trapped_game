using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YipliFMDriverCommunication;

public class YMenuManager : MonoBehaviour
{
    //required variables
    [SerializeField] Button[] menuButtons;
    [SerializeField] GameObject niText;

    [SerializeField] PlayerStats ps;

    Button currentB;

    int currentButtonIndex;

    const string LEFT = "left";
    const string RIGHT = "right";
    const string ENTER = "enter";
    const float waitTime = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        MatControlsStatManager.gameStateChanged(GameState.GAME_UI);

        StartCoroutine(FindObjectOfType<Transition>().FadeInScene());

        currentButtonIndex = 0;
        manageCurrentButton();

        niText.SetActive(false);

        ps.CheckPointPassed = false;
        ps.AllowInput = false;
        ps.PlayerLives = 3;

        if (!ps.InitialiseOldFmResponse)
        {
            ps.InitialiseOldFmResponse = true;
            PlayerSession.Instance.currentYipliConfig.oldFMResponseCount = 0;
        }
    }

    private static void SetClusterIDtoZero()
    {
        try
        {
            Debug.Log("From main menu : Set cluster id to : 0");
            //YipliHelper.SetGameClusterId(0);
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
        for (int i = 0; i < menuButtons.Length; i++)
        {
            if (i == currentButtonIndex)
            {
                //menuButtons[i].GetComponent<Image>().color = Color.green;
                menuButtons[i].GetComponent<Animator>().enabled = true;
                menuButtons[i].transform.GetChild(0).gameObject.SetActive(true);
                currentB = menuButtons[i];
            }
            else
            {
                //menuButtons[i].GetComponent<Image>().color = Color.white;
                menuButtons[i].GetComponent<Animator>().enabled = false;
                menuButtons[i].transform.GetChild(0).gameObject.SetActive(false);
                menuButtons[i].transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void MenuControlSystem()
    {
        string fmActionData = InitBLE.GetFMResponse();
        Debug.Log("Json Data from Fmdriver in matinput : " + fmActionData);

        FmDriverResponseInfo singlePlayerResponse = null;

        try
        {
            singlePlayerResponse = JsonUtility.FromJson<FmDriverResponseInfo>(fmActionData);
        }
        catch (System.Exception e)
        {
            Debug.Log("singlePlayerResponse is having problem : " + e.Message);
        }

        if (singlePlayerResponse == null) return;

        if (PlayerSession.Instance.currentYipliConfig.oldFMResponseCount != singlePlayerResponse.count)
        {
            //Debug.Log("FMResponse " + fmActionData);
            PlayerSession.Instance.currentYipliConfig.oldFMResponseCount = singlePlayerResponse.count;

            YipliUtils.PlayerActions providedAction = ActionAndGameInfoManager.GetActionEnumFromActionID(singlePlayerResponse.playerdata[0].fmresponse.action_id);

            switch(providedAction)
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

    public void ShowNoInternetText()
    {
        niText.SetActive(true);
    }

    public void CloseNoInternetText()
    {
        niText.SetActive(false);
    }

    public void LoadTutorial()
    {
        ps.IsStartTextShown = false;
        SceneManager.LoadScene("Level_Tutorial");
    }

    public void LoadTroubleShootScene()
    {
        SceneManager.LoadScene("Troubleshooting");
    }
}
