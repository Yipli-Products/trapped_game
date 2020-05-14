using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class YSessionManager : MonoBehaviour
{
    [SerializeField] PlayerStats ps;

    int completed_levels;
    int coinScore;

     // Start is called before the first frame update
    void Start()
    {
        if (YipliHelper.checkInternetConnection())
        {
            PlayerSession.Instance.StartSPSession("trapped");
        }
        
        completed_levels = ps.GetCompletedLevels();
        coinScore = ps.GetCoinScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (YipliHelper.checkInternetConnection())
        {
            PlayerSession.Instance.UpdateDuration();
        }
    }

    public void StoreSession() {

        if (ps.GetCompletedLevels() != completed_levels || ps.GetCoinScore() > coinScore)
        {
            Dictionary<string, string> gameData;
            gameData = new Dictionary<string, string>();
            gameData.Add("completed-levels", ps.GetCompletedLevels().ToString());
            
            if (PlayerPrefs.GetInt("Coins") > coinScore)
            {
                gameData.Add("coins-collected", ps.GetCoinScore().ToString());
            }
            else
            {
                gameData.Add("coins-collected", coinScore.ToString());
            }
            

            PlayerSession.Instance.UpdateGameData(gameData);
            Debug.Log("Game data is updated successfully.");
        }

        PlayerSession.Instance.StoreSPSession(PlayerPrefs.GetInt("Coins"));
    }
}
