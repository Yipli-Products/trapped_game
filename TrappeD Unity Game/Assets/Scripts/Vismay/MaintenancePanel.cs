﻿using UnityEngine;
using TMPro;

public class MaintenancePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI message;
    //[SerializeField] TextMeshProUGUI title;

    [SerializeField] GameObject maintenancePanel;
    //[SerializeField] GameObject updateButton;

    [SerializeField] YipliConfig currentYipliConfig;

    [SerializeField] NewUIManager newUIManager = null;

    private void Start()
    {
        maintenancePanel.SetActive(false);
        newUIManager.TurnOffMainCommonButton();
        //updateButton.SetActive(false);
    }

    private void Update()
    {
        ManageManitanenceOrBlocking();
        //BlockIfTroubleShootingIsOn();
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    private void ManageManitanenceOrBlocking()
    {
        if (currentYipliConfig.gameInventoryInfo == null) return;

        if (currentYipliConfig.gameInventoryInfo.isGameUnderMaintenance == 1)
        {
            //message.text = char.ToUpper(currentYipliConfig.gameId[0]) + currentYipliConfig.gameId.Substring(1) + " is under maintanance." + "\n\nStay tuned.";
            message.text = currentYipliConfig.gameInventoryInfo.maintenanceMessage;
            //title.text = "Maintenance Notice";

            newUIManager.UpdateButtonDisplay(maintenancePanel.tag, true);
            maintenancePanel.SetActive(true);
            return;
        }

#if UNITY_ANDROID
        if (currentYipliConfig.isDeviceAndroidTV) {
            BlockVersionCheck(currentYipliConfig.gameInventoryInfo.androidTVMinVersion);
        } else {
            BlockVersionCheck(currentYipliConfig.gameInventoryInfo.androidMinVersion);
        }
#elif UNITY_IOS
        BlockVersionCheck(currentYipliConfig.gameInventoryInfo.iosMinVersion);
#elif UNITY_STANDALONE_WIN
        BlockVersionCheck(currentYipliConfig.gameInventoryInfo.winMinVersion);
#endif
    }

    private void BlockVersionCheck(string versionString)
    {
        if (versionString.Equals(",", System.StringComparison.OrdinalIgnoreCase)) return;

        int gameVersionCode = YipliHelper.convertGameVersionToBundleVersionCode(Application.version);
        int notAllowedVersionCode = YipliHelper.convertGameVersionToBundleVersionCode(versionString);

        if (notAllowedVersionCode > gameVersionCode)
        {
            message.text = currentYipliConfig.gameInventoryInfo.versionUpdateMessage;
            //title.text = "Update Notice";

            maintenancePanel.SetActive(true);
            //updateButton.SetActive(true);

            newUIManager.UpdateButtonDisplay(maintenancePanel.tag);
        }
        else
        {
            maintenancePanel.SetActive(false);
            //newUIManager.TurnOffMainCommonButton();
        }
    }

    private void BlockIfTroubleShootingIsOn()
    {
        if (currentYipliConfig.thisUserTicketInfo.ticketStatus == 0) return;

#if UNITY_ANDROID || UNITY_IOS
        TroubleShootBlockCheck(currentYipliConfig.thisUserTicketInfo.bleTest);
#elif UNITY_STANDALONE_WIN
        TroubleShootBlockCheck(currentYipliConfig.thisUserTicketInfo.usbTest);
#endif
    }

    private void TroubleShootBlockCheck(string testStatusToCheck)
    {
        if (testStatusToCheck.Equals("done", System.StringComparison.OrdinalIgnoreCase))
        {
            message.text = "Your environment is currently under troubleshooting. The game is not playable here at this time.";
            //title.text = "Troubleshoot Notice";

            maintenancePanel.SetActive(true);
        }
        else
        {
            maintenancePanel.SetActive(false);
            //newUIManager.TurnOffMainCommonButton();
        }
    }
}