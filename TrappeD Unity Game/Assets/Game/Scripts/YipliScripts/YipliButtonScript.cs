using Firebase.Database;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            if (PlayerSession.Instance.currentYipliConfig.playerInfo.playerId != ps.PlayerID)
            {
                LoadingPanel.SetActive(true);
                await GetPlayerData();
                LoadingPanel.SetActive(false);
            }

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
        }

        ps.SetPlayerName(PlayerSession.Instance.GetCurrentPlayer());
        ps.PlayerID = PlayerSession.Instance.currentYipliConfig.playerInfo.playerId;

        playerName.text = ps.GetPlayerName();
        pointScore.text = "Points : " + ps.GetCoinScore();

        ps.TimePlayed = 0;
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
        YipliHelper.GoToYipli();
    }

    public void GoToStore()
    {
        SceneManager.LoadScene("Store");
    }

    private async Task GetPlayerData ()
    {
        DataSnapshot dataSnapshot = await PlayerSession.Instance.GetGameData("trapped");
        GameData gameData = new GameData();

        try
        {
            if (dataSnapshot != null)
            {
                if (dataSnapshot.Child("active-ball").Value == null)
                {
                    gameData.Active_ball = 0;
                }
                else
                {
                    gameData.Active_ball = int.Parse(dataSnapshot.Child("active-ball").Value.ToString());
                }

                if (dataSnapshot.Child("balls-purchased").Value == null)
                {
                    gameData.Balls_purchased = "";
                }
                else
                {
                    gameData.Balls_purchased = dataSnapshot.Child("balls-purchased").Value.ToString();
                }                
               
                gameData.SetCoinScore(int.Parse(dataSnapshot.Child("coins-collected").Value.ToString()));
                gameData.SetCompletedLevels(Convert.ToInt32(dataSnapshot.Child("completed-levels").Value.ToString()));

                ps.Active_ball = gameData.Active_ball;
                ps.PurchasedBalls = gameData.Balls_purchased;
                ps.SetCoinScore(gameData.GetCoinScore());
                ps.SetCompletedLevels(gameData.GetCompletedLevels());
            }
            else
            {
                gameData.Active_ball = 0;
                gameData.Balls_purchased = "";
                gameData.SetCoinScore(0);
                gameData.SetCompletedLevels(0);

                ps.Active_ball = gameData.Active_ball;
                ps.PurchasedBalls = gameData.Balls_purchased;
                ps.SetCoinScore(gameData.GetCoinScore());
                ps.SetCompletedLevels(gameData.GetCompletedLevels());
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Something is wrong : " + e.Message);

            gameData.Active_ball = 0;
            gameData.Balls_purchased = "";
            gameData.SetCoinScore(0);
            gameData.SetCompletedLevels(0);

            ps.Active_ball = gameData.Active_ball;
            ps.PurchasedBalls = gameData.Balls_purchased;
            ps.SetCoinScore(gameData.GetCoinScore());
            ps.SetCompletedLevels(gameData.GetCompletedLevels());
        }
    }
}

public class GameData
{
    int coinScore;
    int completedLevels;
    string balls_purchased;
    int active_ball;

    public string Balls_purchased { get => balls_purchased; set => balls_purchased = value; }
    public int Active_ball { get => active_ball; set => active_ball = value; }

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
