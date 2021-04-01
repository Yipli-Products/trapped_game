using UnityEngine;
using Unity.RemoteConfig;
using TMPro;

public class MaintenancePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI message;
    [SerializeField] GameObject maintenancePanel;
    [SerializeField] YipliConfig currentYipliConfig;

    public struct userAttributes { }
    public struct appAtrributes { }

    private bool isGameUnderMaintanance = false;
    private string gameNameThatisUnderMaintenance = "";
    private string maintenanceMessage = "";

    // game update part
    private string trapped = string.Empty;
    private string eggcatcher = string.Empty;
    private string joyfuljumps = string.Empty;
    private string metrorush = string.Empty;
    private string multiplayermayhem = string.Empty;
    private string rollingball = string.Empty;
    private string skater = string.Empty;
    private string matbeats = string.Empty;
    private string gameUpdateMessage = string.Empty;

    public bool IsGameUnderMaintanance { get => isGameUnderMaintanance; set => isGameUnderMaintanance = value; }
    public string GameNameThatisUnderMaintenance { get => gameNameThatisUnderMaintenance; set => gameNameThatisUnderMaintenance = value; }
    public string MaintenanceMessage { get => maintenanceMessage; set => maintenanceMessage = value; }
    public string GameUpdateMessage { get => gameUpdateMessage; set => gameUpdateMessage = value; }

    private void Awake()
    {
        ConfigManager.FetchCompleted += ManagePanel;
        ConfigManager.FetchConfigs<userAttributes, appAtrributes>(new userAttributes(), new appAtrributes());
    }

    private void Start()
    {
        maintenancePanel.SetActive(false);
    }

    private void Update()
    {
        ConfigManager.FetchConfigs<userAttributes, appAtrributes>(new userAttributes(), new appAtrributes());
    }

    private void OnDestroy()
    {
        ConfigManager.FetchCompleted -= ManagePanel;
    }

    private void ManagePanel(ConfigResponse response)
    {
        IsGameUnderMaintanance = ConfigManager.appConfig.GetBool("isGameUnderMaintenance");

        //if (!IsGameUnderMaintanance) return;

        GameNameThatisUnderMaintenance = ConfigManager.appConfig.GetString("gameNameThatisUndermaintenance");
        MaintenanceMessage = ConfigManager.appConfig.GetString("maintenanceMessage");
        GameUpdateMessage = ConfigManager.appConfig.GetString("GameUpdateMessage");

        string[] gameIds = GameNameThatisUnderMaintenance.Split(',');

        foreach (string game in gameIds)
        {
            if (IsGameUnderMaintanance && game.Equals(currentYipliConfig.gameId))
            {
                message.text = char.ToUpper(GameNameThatisUnderMaintenance[0]) + GameNameThatisUnderMaintenance.Substring(1) + " is under maintanance.\n\n" + MaintenanceMessage + "\n\nStay tuned.";
                maintenancePanel.SetActive(true);
            }
            else
            {
                maintenancePanel.SetActive(false);
            }
        }

        // sample update blck message
        // win,ios,android:2.00.100,2.00.101,2.00.102
        // only android update string - win,ios,android:0,0,2.00.102
        switch (currentYipliConfig.gameId)
        {
            case "trapped":
                trapped = ConfigManager.appConfig.GetString("trapped");

                Debug.LogError("trapped values : " + trapped);

                if (trapped != null && trapped != "")
                {
                    GameUpdateBlocker(trapped);
                }
                break;

            case "eggcatcher":
                eggcatcher = ConfigManager.appConfig.GetString("eggcatcher");

                if (eggcatcher != null || eggcatcher != "")
                {
                    GameUpdateBlocker(eggcatcher);
                }
                break;

            case "joyfuljumps":
                joyfuljumps = ConfigManager.appConfig.GetString("joyfuljumps");

                if (joyfuljumps != null || joyfuljumps != "")
                {
                    GameUpdateBlocker(joyfuljumps);
                }
                break;

            case "metrorush":
                metrorush = ConfigManager.appConfig.GetString("metrorush");

                if (metrorush != null || metrorush != "")
                {
                    GameUpdateBlocker(metrorush);
                }
                break;

            case "multiplayermayhem":
                multiplayermayhem = ConfigManager.appConfig.GetString("multiplayermayhem");

                if (multiplayermayhem != null || multiplayermayhem != "")
                {
                    GameUpdateBlocker(multiplayermayhem);
                }
                break;

            case "rollingball":
                rollingball = ConfigManager.appConfig.GetString("rollingball");

                if (rollingball != null || rollingball != "")
                {
                    GameUpdateBlocker(rollingball);
                }
                break;

            case "skater":
                skater = ConfigManager.appConfig.GetString("skater");

                if (skater != null || skater != "")
                {
                    GameUpdateBlocker(skater);
                }
                break;

            case "matbeats":
                matbeats = ConfigManager.appConfig.GetString("matbeats");

                if (matbeats != null || matbeats != "")
                {
                    GameUpdateBlocker(matbeats);
                }
                break;
        }
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    // sample update blck message
    // win,ios,android:2.00.100,2.00.101,2.00.102
    // only android update string - win,ios,android:0,0,2.00.102
    private void GameUpdateBlocker(string gameToCheck)
    {
        Debug.LogError("gameToCheck : " + gameToCheck);

        string[] osAndfVersionSplits = gameToCheck.Split(':');

        string[] osSplits = osAndfVersionSplits[0].Split(',');
        string[] versionSplits = osAndfVersionSplits[1].Split(',');

        foreach (string s in osSplits)
        {
#if UNITY_STANDALONE_WIN
            if (s.Equals("win", System.StringComparison.OrdinalIgnoreCase))
            {
                int gameVersionCode = YipliHelper.convertGameVersionToBundleVersionCode(Application.version);
                int notAllowedVersionCode = YipliHelper.convertGameVersionToBundleVersionCode(versionSplits[0]);

                if (notAllowedVersionCode > gameVersionCode)
                {
                    message.text = "Please update the game. Old versions are not supported anymore.\nThank you";
                    maintenancePanel.SetActive(true);
                }
                else
                {
                    maintenancePanel.SetActive(false);
                }
            }
#elif UNITY_IOS
            if (s.Equals("ios", System.StringComparison.OrdinalIgnoreCase))
            {
                int gameVersionCode = YipliHelper.convertGameVersionToBundleVersionCode(Application.version);
                int notAllowedVersionCode = YipliHelper.convertGameVersionToBundleVersionCode(versionSplits[1]);

                if (notAllowedVersionCode > gameVersionCode)
                {
                    message.text = "Please update the game. Old versions are not supported anymore.\nThank you";
                    maintenancePanel.SetActive(true);
                }
                else
                {
                    maintenancePanel.SetActive(false);
                }
            }
#elif UNITY_ANDROID
            if (s.Equals("android", System.StringComparison.OrdinalIgnoreCase))
            {
                int gameVersionCode = YipliHelper.convertGameVersionToBundleVersionCode(Application.version);
                int notAllowedVersionCode = YipliHelper.convertGameVersionToBundleVersionCode(versionSplits[2]);

                if (notAllowedVersionCode > gameVersionCode)
                {
                    message.text = "Please update the game. Old versions are not supported anymore.\nThank you";
                    maintenancePanel.SetActive(true);
                }
                else
                {
                    maintenancePanel.SetActive(false);
                }
            }
#endif
        }
    }
}
