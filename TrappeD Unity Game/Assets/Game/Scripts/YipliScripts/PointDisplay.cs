using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointDisplay : MonoBehaviour
{
    [SerializeField] Text pointScore;
    [SerializeField] Text timePlayedText;
    [SerializeField] Text cbText;
    [SerializeField] Text fpText;

    [SerializeField] Text tipsText;

    [SerializeField] PlayerStats ps;

    // Start is called before the first frame update
    void Start()
    {
        //pointScore.text = "Collected Coins : " + PlayerPrefs.GetInt("thisLevelPoints");
        pointScore.text = "Coins collected : " + ps.GetCoinScore();

        if (SceneManager.GetActiveScene().name == "Game Over")
        {
            /*
            timePlayedText.text = "Time Played : " + ((int)(ps.ThisSessionTimePlayed / 60)).ToString("00") + ":" + ((int)(ps.ThisSessionTimePlayed % 60)).ToString("00");
            cbText.text = "Calories Burnt : " + ps.ThisSessionCalBurned;
            fpText.text = "Fitness Points : " + ps.ThisSessionFpPoints;
            */

            tipsText.text = ps.Tips[Random.Range(0, ps.Tips.Length)];

            ps.TimePlayed = 0;
            ps.CalBurned = 0;
            ps.FpPoints = 0;

            ps.ThisSessionTimePlayed = 0;
            ps.ThisSessionCalBurned = 0;
            ps.ThisSessionFpPoints = 0;
        }
        else
        {
            timePlayedText.text = "Time Played : " + ((int)(ps.TimePlayed / 60)).ToString("00") + ":" + ((int)(ps.TimePlayed % 60)).ToString("00");
            cbText.text = "Calories Burnt : " + ps.CalBurned;
            fpText.text = "Fitness Points : " + ps.FpPoints;

            ps.TimePlayed = 0;
            ps.CalBurned = 0;
            ps.FpPoints = 0;
        }
    }
}