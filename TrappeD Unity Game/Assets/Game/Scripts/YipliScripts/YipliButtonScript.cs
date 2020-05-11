using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YipliButtonScript : MonoBehaviour
{
    [SerializeField] Text playerName;
    [SerializeField] Text pointScore;
    [SerializeField] PlayerStats ps;

    private void Start()
    {
        //PlayerPrefs.SetInt("Coins", 0); // activate only if score is required to set to 0.

        ps.SetCompletedLevels(PlayerPrefs.GetInt("NUMBER_OF_COMP_LEVELS"));
        ps.SetCoinScore(PlayerPrefs.GetInt("Coins"));
        ps.SetPlayerName(PlayerSession.Instance.GetCurrentPlayer());

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
}
