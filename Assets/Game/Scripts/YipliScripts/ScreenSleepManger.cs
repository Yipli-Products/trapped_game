using System.Collections;
using System.Collections.Generic;
//using FMInterface_Windows;
using UnityEngine;

public class ScreenSleepManger : MonoBehaviour
{
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int st = FindObjectsOfType<ScreenSleepManger>().Length;
        if (st > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    /*
    void OnApplicationQuit()
    {
        Debug.LogError("Inside OnApplicationQuit");
        DeviceControlActivity._disconnect();
        DeviceControlActivity.readThread.Abort();
    }
    */
}
