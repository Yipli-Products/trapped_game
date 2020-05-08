using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreYSession : MonoBehaviour
{
    //required variables
    private StartGameLibSession sgls;

    // Start is called before the first frame update
    void Start()
    {
        sgls = FindObjectOfType<StartGameLibSession>();

        StoreYipliSession();
    }

    private void StoreYipliSession()
    {
        PlayerSession.Instance.StoreSPSession(PlayerPrefs.GetInt("Coins"));
        sgls.SessionStartStatus = false;
    }
}
