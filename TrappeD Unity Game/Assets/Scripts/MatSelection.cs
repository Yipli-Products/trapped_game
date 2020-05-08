using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatSelection : MonoBehaviour
{
    public TextMeshProUGUI noMatText;
    public TextMeshProUGUI passwordErrorText;
    public InputField inputPassword;
    public GameObject loadingPanel;
    private YipliMatInfo yipliMat = new YipliMatInfo();

    public GameObject NoMatPanel;
    public GameObject secretEntryPanel;
    public YipliConfig currentYipliConfig;
    private string connectionState;
    private int checkMatStatusCount;

    public void CheckMatConnectionStatus(string user)
    {
        Debug.Log("Checking Mat.");

        NoMatPanel.SetActive(false);

        string connectionState = "";
        if (yipliMat != null && yipliMat.matMacId.Length > 0)
        {
            connectionState = InitBLE.getBLEStatus();
            //uncomment blemow to to skip mat
            //connectionState = "CONNECTED";
            if (connectionState == "CONNECTED")
            {
                FindObjectOfType<YipliAudioManager>().Play("BLE_success");
                //load last Scene
                SceneManager.LoadScene(currentYipliConfig.callbackLevel);
            }
            else
            {
                FindObjectOfType<YipliAudioManager>().Play("BLE_failure");
                Debug.Log("Mat not reachable.");
                noMatText.text = "Make sure that the registered mat is reachable.";
                NoMatPanel.SetActive(true);
            }
        }
        else //Current Mat not found in Db.
        {
            FindObjectOfType<YipliAudioManager>().Play("BLE_failure");
            Debug.Log("No Mat found in cache.");
            NoMatPanel.SetActive(true);
        }
    }

    public void SkipMat()
    {
        NoMatPanel.SetActive(false);
        passwordErrorText.text = "";
        inputPassword.text = "";
        secretEntryPanel.SetActive(true);
    }

    public void OnPlayPress()
    {
        if (inputPassword.text == "123456")
        {
            FindObjectOfType<YipliAudioManager>().Play("BLE_success");
            //load last Scene
            SceneManager.LoadScene(currentYipliConfig.callbackLevel);
        }
        else
        {
            FindObjectOfType<YipliAudioManager>().Play("BLE_failure"); inputPassword.text = "";
            passwordErrorText.text = "Invalid pasword";
            Debug.Log("incorrect password");
        }
    }

    public void OnBackPress()
    {
        secretEntryPanel.SetActive(false);
        NoMatPanel.SetActive(true);
    }

    public async void ReCheckMatConnection()
    {
        Debug.Log("Checking Mat.");
        NoMatPanel.SetActive(false);
        string result = "failure";
        //To handle the case of No mats registered
        if ((yipliMat == null) || (yipliMat.matMacId.Length == 0))
        {
            loadingPanel.SetActive(true);
            result = await validateAndConnectMat(currentYipliConfig.userId);
            loadingPanel.SetActive(false);
        }

        string connectionState = "";
        if ((yipliMat != null) || (result == "success"))
        {
            try
            {
                connectionState = InitBLE.getBLEStatus();
            }
            catch (Exception exp)
            {
                Debug.Log("Exception occured in ReCheckMatConnection() : " + exp.Message);
            }
            if (connectionState == "CONNECTED")
            {
                FindObjectOfType<YipliAudioManager>().Play("BLE_success");
                
                //load last Scene
                SceneManager.LoadScene(currentYipliConfig.callbackLevel);
            }
            else
            {
                // If it is > 1, reCheckis clicked atleast once. After ReChecking the status, if the status isnt connected,
                //then initiate the Mat connection again, so that, in next reCheck it will get connected.
                try
                {
                    InitBLE.InitBLEFramework(yipliMat.matMacId);
                }
                catch (Exception exp)
                {
                    Debug.Log("Exception occured in ReCheckMatConnection() : " + exp.Message);
                }
                Debug.Log("Mat not reachable.");
                noMatText.text = "Make sure that the registered mat is reachable.";

                FindObjectOfType<YipliAudioManager>().Play("BLE_failure");
                NoMatPanel.SetActive(true);
            }
        }
        else //Current Mat not found in Db.
        {
            FindObjectOfType<YipliAudioManager>().Play("BLE_failure");
            Debug.Log("No Mat found in cache.");
            NoMatPanel.SetActive(true);
        }
    }

    public async Task<string> validateAndConnectMat(string userId)
    {
        Debug.Log("Starting mat connection");
        try
        {
            yipliMat = await FirebaseDBHandler.GetCurrentMatDetails(userId, () => { Debug.Log("Got the Mat details from db"); });
            if (yipliMat != null)
            {
                if (yipliMat.matMacId != null)
                {
                    Debug.Log("connecting to : " + yipliMat.matName);

                    //Initiate the connection with the mat.
                    currentYipliConfig.matInfo = yipliMat;
                    InitBLE.InitBLEFramework(yipliMat.matMacId);
                    return "success";
                }
                else
                {
                    Debug.Log("No valid yipli mat found. Register a YIPLI mat and try again.");
                }
            }
            else
            {
                Debug.Log("No valid yipli mat found. Register a YIPLI mat and try again.");
            }
        }
        catch (Exception exp)
        {
            Debug.Log("Exception occured in ConnectMat(). Check if the Mat is registered wih Valid Mac ID.");
            Debug.Log(exp.Message);
        }
        return "failure";
    }


    public void OnGoToYipliPress()
    {
        string bundleId = "org.hightimeshq.yipli"; //todo: Change this later
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.Mat.UnityMat");
        AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");

        AndroidJavaObject launchIntent = null;
        try
        {
            launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", bundleId);
            ca.Call("startActivity", launchIntent);
        }
        catch (AndroidJavaException e)
        {
            Debug.Log(e);
            noMatText.text = "Yipli App is not installed. Please install Yipli from playstore to proceed.";
        }
    }
}
