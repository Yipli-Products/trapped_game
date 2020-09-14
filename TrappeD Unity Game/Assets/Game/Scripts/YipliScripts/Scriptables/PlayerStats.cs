using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStats/playerData")]
public class PlayerStats : ScriptableObject
{
    public string playerName;
    public int completed_levels;
    public int coinScore;

    private int intended_ball_price = -1;
    private int intended_ball_id = -1;
    [SerializeField] int active_ball = -1;

    [SerializeField] int timePlayed;
    [SerializeField] int calBurned;
    [SerializeField] int fpPoints;

    [SerializeField] string playerID;
    [SerializeField] string purchasedBalls;

    [SerializeField] List<StoreBalls> ballsInStore;

    public int Intended_ball_price { get => intended_ball_price; set => intended_ball_price = value; }
    public int Intended_ball_id { get => intended_ball_id; set => intended_ball_id = value; }
    public int Active_ball { get => active_ball; set => active_ball = value; }
    public int TimePlayed { get => timePlayed; set => timePlayed = value; }
    private List<StoreBalls> BallsInStore { get => ballsInStore; set => ballsInStore = value; }
    public int CalBurned { get => calBurned; set => calBurned = value; }
    public int FpPoints { get => fpPoints; set => fpPoints = value; }
    public string PlayerID { get => playerID; set => playerID = value; }
    public string PurchasedBalls { get => purchasedBalls; set => purchasedBalls = value; }

    public void SetListofBalls(List<int> purchasedIDs)
    {
        foreach(StoreBalls ball in BallsInStore)
        {
            if (purchasedIDs.Contains(ball.ballID))
            {
                ball.boughtStatus = true;
            }
            else
            {
                ball.boughtStatus = false;
            }
        }
    }

    public void SetDefaultStore()
    {
        foreach (StoreBalls ball in BallsInStore)
        {
            if (ball.ballID == 0)
            {
                ball.boughtStatus = true;
                Active_ball = 0;
            }
            else
            {
                ball.boughtStatus = false;
            }
        }
    }

    public void SetPlayerName(string pname)
    {
        playerName = pname;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetCompletedLevels(int CompletedLevels)
    {
        completed_levels = CompletedLevels;
    }

    public int GetCompletedLevels()
    {
        return completed_levels;
    }

    public void SetCoinScore(int cs)
    {
        coinScore = cs;
    }

    public int GetCoinScore()
    {
        return coinScore;
    }

    public int GetBallPrice(int idBall)
    {
        foreach (StoreBalls ball in BallsInStore)
        {
            if (ball.ballID == idBall)
            {
                return ball.ballPrice;
            }
        }
        return -5;
    }

    public bool GetBallBoughtStatus(int idBall)
    {
        foreach (StoreBalls ball in BallsInStore)
        {
            if (ball.ballID == idBall)
            {
                return ball.boughtStatus;
            }
        }
        return false;
    }

    public void SetBallBoughtStatus(int idBall, bool boughtStatus)
    {
        foreach (StoreBalls ball in BallsInStore)
        {
            if (ball.ballID == idBall)
            {
                ball.boughtStatus = boughtStatus;
            }
        }
    }

    [System.Serializable]
    public class StoreBalls
    {
        public int ballID;
        public int ballPrice;
        public bool boughtStatus;
    }
}
