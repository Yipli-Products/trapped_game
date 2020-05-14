using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScoreManager : MonoBehaviour
{
    /* private string level_one = "Level_01_RC";
     private string level_two = "Level_02_RC";
     private string level_three = "Level_03_RC";
     private string level_four = "Level_04_RC";
     private string level_five = "Level_05_RC";
     private string level_six = "Level_06_RC";
     private string level_seven = "Level_07_rc";
     private string level_eight = "Level_08_RC";
     private string level_nine = "Level_09_RC"; */

    [SerializeField] Text playerName;
    [SerializeField] PlayerStats ps;

    private int level_one_start = 9;
    private int level_two_start = 5;
    private int level_three_start = 3;
    private int level_four_start = 9;
    private int level_five_start = 9;
    private int level_six_start = 8;
    private int level_seven_start = 8;
    private int level_eight_start = 8;
    private int level_nine_start = 12;

    private string currentLevel;
    private int currentStars;

    private void Start()
    {
        playerName.text = ps.GetPlayerName();
        SetCurrentStarts();
    }

    public void SetCurrentStarts ()
    {
        currentLevel = SceneManager.GetActiveScene().name;

        switch (currentLevel)
        {
            case "Level_01_RC":
                currentStars = level_one_start;
                PlayerPrefs.SetInt("1", 0);
                PlayerPrefs.SetInt("currentStars", currentStars);
                break;

            case "Level_02_RC":
                currentStars = level_two_start;
                PlayerPrefs.SetInt("currentStars", currentStars);
                break;

            case "Level_03_RC":
                currentStars = level_three_start;
                PlayerPrefs.SetInt("currentStars", currentStars);
                break;

            case "Level_04_RC":
                currentStars = level_four_start;
                PlayerPrefs.SetInt("currentStars", currentStars);
                break;

            case "Level_05_RC":
                currentStars = level_five_start;
                PlayerPrefs.SetInt("currentStars", currentStars);
                break;

            case "Level_06_RC":
                currentStars = level_six_start;
                PlayerPrefs.SetInt("currentStars", currentStars);
                break;

            case "Level_07_rc":
                currentStars = level_seven_start;
                PlayerPrefs.SetInt("currentStars", currentStars);
                break;

            case "Level_08_RC":
                currentStars = level_eight_start;
                PlayerPrefs.SetInt("currentStars", currentStars);
                break;

            case "Level_09_RC":
                currentStars = level_nine_start;
                PlayerPrefs.SetInt("currentStars", currentStars);
                break;
        }
    }
}
