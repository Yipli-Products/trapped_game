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

    private void Start()
    {
        NoInternetIcon.SetActive(false);

        if (YipliHelper.checkInternetConnection())
        {
            //LoadingPanel.SetActive(true);
            GetPlayerData();
            //LoadingPanel.SetActive(false);
        }
        else
        {
            ps.SetCoinScore(0);
            ps.SetCompletedLevels(0);
            NoInternetIcon.SetActive(true);
        }

        ps.SetPlayerName(PlayerSession.Instance.GetCurrentPlayer());
        ps.PlayerID = PlayerSession.Instance.currentYipliConfig.playerInfo.playerId;

        if (ps.GetPlayerName().Length > 10)
        {
            playerName.text = ps.GetPlayerName().Substring(0, 9);
        }
        else
        {
            playerName.text = ps.GetPlayerName();
        }

        pointScore.text = ps.GetCoinScore().ToString();

        ps.TimePlayed = 0;
    }

    private void Update()
    {
        playerName.text = ps.GetPlayerName();
        pointScore.text = ps.GetCoinScore().ToString();
    }

    public void ChangePlayer()
    {
        PlayerSession.Instance.ChangePlayer();
    }

    public void GoToYipli()
    {
        YipliHelper.GoToYipli();
    }

    public void GoToTroubleShooting()
    {
        PlayerSession.Instance.TroubleShootSystem();
    }

    public void GoToStore()
    {
        if (YipliHelper.checkInternetConnection())
        {
            SceneManager.LoadScene("Store");
        }
        else
        {
            NoInternetIcon.SetActive(true);
        }
    }

    private void GetPlayerData ()
    {
        DataSnapshot dataSnapshot = PlayerSession.Instance.GetGameData();
        GameData gameData = new GameData();

        try
        {
            if (dataSnapshot.Value != null)
            {
                ps.IsTutorialMandatory = false;

                // active ball
                if (dataSnapshot.Child("active-ball").Value == null)
                {
                    gameData.Active_ball = 0;
                }
                else
                {
                    gameData.Active_ball = int.Parse(dataSnapshot.Child("active-ball").Value.ToString());
                }

                // store data
                if (dataSnapshot.Child("balls-purchased").Value == null)
                {
                    gameData.Balls_purchased = "";
                }
                else
                {
                    gameData.Balls_purchased = dataSnapshot.Child("balls-purchased").Value.ToString();
                }

                // coins - collected
                if (dataSnapshot.Child("coins-collected").Value == null)
                {
                    gameData.SetCoinScore(0);
                }
                else
                {
                    gameData.SetCoinScore(int.Parse(dataSnapshot.Child("coins-collected").Value.ToString()));
                }

                // levels completed
                if (dataSnapshot.Child("completed-levels").Value == null)
                {
                    gameData.SetCompletedLevels(0);
                }
                else
                {
                    gameData.SetCompletedLevels(Convert.ToInt32(dataSnapshot.Child("completed-levels").Value.ToString()));
                }

                ps.Active_ball = gameData.Active_ball;
                ps.PurchasedBalls = gameData.Balls_purchased;
                ps.SetCoinScore(gameData.GetCoinScore());
                ps.SetCompletedLevels(gameData.GetCompletedLevels());
            }
            else
            {
                ps.isTutorialMandatory = true;

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

            ps.isTutorialMandatory = true;

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
