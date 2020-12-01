using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YipliFMDriverCommunication;

public class EndManager : MonoBehaviour
{
    [SerializeField] GameObject endNotes;
    [SerializeField] GameObject endCol;
    
    void Start()
    {
        endNotes.SetActive(false);
        endCol.SetActive(false);
    }

    public void ActiveEndNote()
    {
        endNotes.SetActive(true);
        endCol.SetActive(true);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        Debug.LogError("endNotes : " + endNotes.activeSelf);
        if (endNotes.activeSelf)
        {
            MenuControlSystem();
        }
    }

    private void MenuControlSystem()
    {
        string fmActionData = InitBLE.GetFMResponse();
        Debug.Log("Json Data from Fmdriver : " + fmActionData);

        FmDriverResponseInfo singlePlayerResponse = JsonUtility.FromJson<FmDriverResponseInfo>(fmActionData);

        if (singlePlayerResponse == null) return;

        if (PlayerSession.Instance.currentYipliConfig.oldFMResponseCount < singlePlayerResponse.count)
        {
            Debug.Log("FMResponse " + fmActionData);
            PlayerSession.Instance.currentYipliConfig.oldFMResponseCount = singlePlayerResponse.count;

            YipliUtils.PlayerActions providedAction = ActionAndGameInfoManager.GetActionEnumFromActionID(singlePlayerResponse.playerdata[0].fmresponse.action_id);

            switch (providedAction)
            {
                case YipliUtils.PlayerActions.JUMP:
                    GoToMainMenu();
                    break;

                case YipliUtils.PlayerActions.ENTER:
                    GoToMainMenu();
                    break;
            }
        }
    }
}
