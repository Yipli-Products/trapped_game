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

    LoadLevelByName llbn;
    MainMenuBtnController mmbc;

    bool leftPressed = false;
    bool rightPressed = false;
    bool EnterPressed = false;

    string FMResponseCount = "";

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        llbn = FindObjectOfType<LoadLevelByName>();
        mmbc = FindObjectOfType<MainMenuBtnController>();

        currentButtonIndex = 0;
        manageCurrentButton();

        PlayerSession.Instance.SetGameClusterId(0);
    }

    // Update is called once per frame
    void Update()
    {
        MenuControlSystem();
        
        GetMatKeyInputs();

        CalculateTime();
    }

    private void CalculateTime()
    {
        timer += Time.deltaTime;
    }

    private void GetMatKeyInputs()
    {
        // left to right play, changeplayer, gotoyipli, exit
        if (Input.GetKeyDown(KeyCode.LeftArrow) || leftPressed)
        {
            currentButtonIndex = GetPreviousButton();
            manageCurrentButton();

            leftPressed = false;
        }

        // left to right play, changeplayer, gotoyipli, exit
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
        // timer conditions to map with 1s time
        if (timer > 1f)
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
                if (FMTokens[1] == "Pause")
                {
                    
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
}
