using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointDisplay : MonoBehaviour
{
    [SerializeField] Text pointScore;
    [SerializeField] Text coinscoreTotal;
    [SerializeField] Text timePlayedText;
    [SerializeField] Text cbText;
    [SerializeField] Text fpText;

    [SerializeField] PlayerStats ps;

    // Start is called before the first frame update
    void Start()
    {
        pointScore.text = "Collected Coins : " + PlayerPrefs.GetInt("thisLevelPoints");
        
        if (SceneManager.GetActiveScene().name == "Game Won")
        {
            coinscoreTotal.text = "Collected Coins : " + ps.GetCoinScore();
        }

        if (SceneManager.GetActiveScene().name == "Game Over")
        {
            timePlayedText.text = "Time Played : " + ((int)(ps.TimePlayed / 60)).ToString("00") + ":" + ((int)(ps.TimePlayed % 60)).ToString("00");
            cbText.text = "Calories Burnt : " + ps.CalBurned;
            fpText.text = "Fitness Points : " + ps.FpPoints;

            ps.TimePlayed = 0;
        }
    }
}