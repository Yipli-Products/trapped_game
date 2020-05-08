using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointDisplay : MonoBehaviour
{
    [SerializeField] Text pointScoreTotal;
    [SerializeField] Text pointScore;

    // Start is called before the first frame update
    void Start()
    {
        pointScoreTotal.text = "Total Points : " + PlayerPrefs.GetInt("Coins");
        pointScore.text = "Collected Points : " + PlayerPrefs.GetInt("thisLevelPoints");
    }
}