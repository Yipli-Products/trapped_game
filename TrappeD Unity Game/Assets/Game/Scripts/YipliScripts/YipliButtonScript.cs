using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class YipliButtonScript : MonoBehaviour
{
    [SerializeField] Text playerName;
    [SerializeField] Text pointScore;
    [SerializeField] PlayerStats ps;
    [SerializeField] GameObject NoInternetIcon;
    [SerializeField] GameObject LoadingPanel;


    private async void Start()
    {
        NoInternetIcon.SetActive(false);

        //PlayerPrefs.DeleteAll(); // reset player prefs
        PlayerPrefs.DeleteKey("IS_CHKP_REACHED");
        PlayerPrefs.DeleteKey("CHKP_X");
        PlayerPrefs.DeleteKey("CHKP_Y");
        PlayerPrefs.DeleteKey("CHKP_Z");

        if (YipliHelper.checkInternetConnection())
        {
            LoadingPanel.SetActive(true);
            await GetPlayerData();
            LoadingPanel.SetActive(false);

            //Task.Run(GetPlayerData).Wait();
        }
        else
        {
            ps.SetCoinScore(0);
            ps.SetCompletedLevels(0);
            NoInternetIcon.SetActive(true);

            //print("internet connection false");
        }

        /*ps.SetCompletedLevels(PlayerPrefs.GetInt("NUMBER_OF_COMP_LEVELS"));
        ps.SetCoinScore(PlayerPrefs.GetInt("Coins")); */

        /*Debug.Log("coin score : " + ps.GetCoinScore());
        Debug.Log("Completed levels : " + ps.GetCompletedLevels()); */

        ps.SetPlayerName(PlayerSession.Instance.GetCurrentPlayer());

        playerName.text = ps.GetPlayerName();
        pointScore.text = "Points : " + ps.GetCoinScore();
    }

    private void Update()
    {
        playerName.text = ps.GetPlayerName();
        pointScore.text = "Points : " + ps.GetCoinScore();
    }

    public void ChangePlayer()
    {
        PlayerSession.Instance.ChangePlayer();
    }

    public void GoToYipli()
    {
        PlayerSession.Instance.GoToYipli();
    }

    private async Task GetPlayerData ()
    {
        DataSnapshot dataSnapshot = await PlayerSession.Instance.GetGameData("trapped");
        GameData gameData = new GameData();

        try
        {
            if (dataSnapshot != null)
            {
                gameData.SetCoinScore(int.Parse(dataSnapshot.Child("coins-collected").Value.ToString()));

                Debug.Log("CoinScore from try : " + gameData.GetCoinScore());

                gameData.SetCompletedLevels(Convert.ToInt32(dataSnapshot.Child("completed-levels").Value.ToString()));

                Debug.Log("Completed levels from try : " + gameData.GetCompletedLevels());

                ps.SetCoinScore(gameData.GetCoinScore());
                ps.SetCompletedLevels(gameData.GetCompletedLevels());
            }
            else
            {
                gameData.SetCoinScore(0);
                gameData.SetCompletedLevels(0);

                ps.SetCoinScore(gameData.GetCoinScore());
                ps.SetCompletedLevels(gameData.GetCompletedLevels());
            }
        }
        catch (Exception e)
        {
            Debug.Log("Something is wrong : " + e.Message);

            gameData.SetCoinScore(0);
            gameData.SetCompletedLevels(0);

            ps.SetCoinScore(gameData.GetCoinScore());
            ps.SetCompletedLevels(gameData.GetCompletedLevels());
        }
    }
}

public class GameData
{
    int coinScore;
    int completedLevels;

    public void SetCompletedLevels(int CompletedLevels)
    {
        completedLevels = CompletedLevels;
    }

    public int GetCompletedLevels()
    {
        return completedLevels;
    }

    public void SetCoinScore(int cs)
    {
        coinScore = cs;
    }

    public int GetCoinScore()
    {
        return coinScore;
    }
}
