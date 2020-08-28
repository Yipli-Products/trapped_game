using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterIDManager : MonoBehaviour
{
    //required variables
    [SerializeField] GameObject lifeOne;
    [SerializeField] GameObject lifeTwo;
    [SerializeField] GameObject lifeThree;

    private void Awake()
    {
        SetClusterIDtoOne();
    }

    // Start is called before the first frame update
    void Start()
    {
        lifeOne.SetActive(false);
        lifeTwo.SetActive(false);
        lifeThree.SetActive(false);
    }

    private static void SetClusterIDtoOne()
    {
        try
        {
            Debug.Log("From retry or resume function : Set cluster id to : 1");
            YipliHelper.SetGameClusterId(1); // set current gameid
        }
        catch (System.Exception e)
        {
            Debug.Log("Retry or Resume function cluster id 1 exception.");
            Debug.Log("Exception : " + e.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
