using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        PlayerSession.Instance.StoreSPSession(PlayerPrefs.GetInt("Coins"));
        SessionStartStatus = false;
    }
}
//trapped