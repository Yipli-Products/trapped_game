using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreYSession : MonoBehaviour
{
    //required variables
    private StartGameLibSession sgls;

    private YSessionManager ysm;

    // Start is called before the first frame update
    void Start()
    {
        sgls = FindObjectOfType<StartGameLibSession>();
        ysm = FindObjectOfType<YSessionManager>();

        StoreYipliSession();
    }

    private void StoreYipliSession()
    {
        ysm.StoreSession();

        sgls.SessionStartStatus = false;
    }
}
