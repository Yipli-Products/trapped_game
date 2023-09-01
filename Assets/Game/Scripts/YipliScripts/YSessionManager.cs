using UnityEngine;
using System.Collections.Generic;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class YSessionManager : MonoBehaviour
{
    [SerializeField] PlayerStats ps;

    int completed_levels;
    int coinScore;

     // Start is called before the first frame update
    void Start()
    {
        PlayerSession.Instance.StartSPSession();

        completed_levels = ps.GetCompletedLevels();
        coinScore = ps.GetCoinScore();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSession.Instance.UpdateDuration();
    }

    public void StoreSession(bool isGameOver = false) 
    {
        if (isGameOver)
        {
            MatControlsStatManager.gameStateChanged(GameState.GAME_OVER);
        }
        else
        {
            MatControlsStatManager.gameStateChanged(GameState.GAME_NEW_LIFE);
        }

        Dictionary<string, string> gameData;
        gameData = new Dictionary<string, string>();
        gameData.Add("completed-levels", ps.GetCompletedLevels().ToString());

        if (ps.GetCoinScore() > coinScore)
        {
            gameData.Add("coins-collected", ps.GetCoinScore().ToString());
        }
        else
        {
            gameData.Add("coins-collected", coinScore.ToString());
        }

        gameData.Add("active-ball", ps.Active_ball.ToString());
        gameData.Add("balls-purchased", ps.PurchasedBalls);

        PlayerSession.Instance.UpdateGameData(gameData);
        Debug.Log("Game data is updated successfully.");

        ps.CalBurned = PlayerSession.Instance.GetCaloriesBurned();
        ps.FpPoints = (int)PlayerSession.Instance.GetFitnessPoints();

        ps.ThisSessionTimePlayed += ps.TimePlayed;
        ps.ThisSessionCalBurned += ps.CalBurned;
        ps.ThisSessionFpPoints += ps.FpPoints;

        /*
        // add running action here
        if (PlayerSession.Instance != null)
        {
            PlayerSession.Instance.AddPlayerAction(YipliUtils.PlayerActions.RUNNING, FindObjectOfType<BallController>().CurrentStepCount);
            Debug.LogError("CurrentStepCount from store session : " + FindObjectOfType<BallController>().CurrentStepCount);
            FindObjectOfType<BallController>().CurrentStepCount = 0;
        }
        */

        PlayerSession.Instance.StoreSPSession(ps.GetCoinScore());
    }
}
