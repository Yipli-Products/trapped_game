using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class StartGameLibSession : MonoBehaviour
{
    // required variables
    public bool SessionStartStatus = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!SessionStartStatus) 
        {
            StartyipliSession();
        }
    }

    private void StartyipliSession () 
    { 
        PlayerSession.Instance.StartSPSession();
        SessionStartStatus = true;
    }

    public void StoreYipliSession () 
    {
        // add running action here
        if (PlayerSession.Instance != null)
        {
            PlayerSession.Instance.AddPlayerAction(YipliUtils.PlayerActions.RUNNING, FindObjectOfType<BallController>().CurrentStepCount);
            FindObjectOfType<BallController>().CurrentStepCount = 0;
        }

        PlayerSession.Instance.StoreSPSession(PlayerPrefs.GetInt("Coins"));
        SessionStartStatus = false;
    }
}
//trapped