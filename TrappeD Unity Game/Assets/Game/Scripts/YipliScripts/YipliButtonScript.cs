using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YipliButtonScript : MonoBehaviour
{
    [SerializeField] Text playerName;
    [SerializeField] Text pointScore;

    private void Start()
    {
        //PlayerPrefs.SetInt("Coins", 0); // activate only if score is required to set to 0.

        playerName.text = PlayerSession.Instance.GetCurrentPlayer();
        pointScore.text = "Points : " + PlayerPrefs.GetInt("Coins");
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
