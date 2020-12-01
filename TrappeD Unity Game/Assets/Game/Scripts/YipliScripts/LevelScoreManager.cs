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
    [SerializeField] Text levelName;
    [SerializeField] PlayerStats ps;

    private int level_one_start = 15; // 150
    private int level_two_start = 16; // 160
    private int level_three_start = 22; // 220
    private int level_four_start = 30; // 300
    private int level_five_start = 16; // 160
    private int level_six_start = 22; // 220
    private int level_seven_start = 18; // 180 
    private int level_eight_start = 16; // 160
    private int level_nine_start = 33; // 330

    private string currentLevel;
    private int currentStars;

    private void Start()
    {
        Time.timeScale = 1f;

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
                levelName.text = "Level 1";
                PlayerPrefs.SetInt("currentStars", currentStars);
                break;

            case "Level_02_RC":
                currentStars = level_two_start;
                levelName.text = "Level 2";
                PlayerPrefs.SetInt("currentStars", currentStars);
                break;

            case "Level_03_RC":
                currentStars = level_three_start;
                levelName.text = "Level 3";
                PlayerPrefs.SetInt("currentStars", currentStars);
                break;

            case "Level_04_RC":
                currentStars = level_four_start;
                levelName.text = "Level 4";
                PlayerPrefs.SetInt("currentStars", currentStars);
                break;

            case "Level_05_RC":
                currentStars = level_five_start;
                levelName.text = "Level 5";
                PlayerPrefs.SetInt("currentStars", currentStars);
                break;

            case "Level_06_RC":
                currentStars = level_six_start;
                levelName.text = "Level 6";
                PlayerPrefs.SetInt("currentStars", currentStars);
                break;

            case "Level_07_rc":
                currentStars = level_seven_start;
                levelName.text = "Level 7";
                PlayerPrefs.SetInt("currentStars", currentStars);
                break;

            case "Level_08_RC":
                currentStars = level_eight_start;
                levelName.text = "Level 8";
                PlayerPrefs.SetInt("currentStars", currentStars);
                break;

            case "Level_09_RC":
                currentStars = level_nine_start;
                levelName.text = "Level 9";
                PlayerPrefs.SetInt("currentStars", currentStars);
                break;

            case "Level_Tutorial":
                levelName.text = "Tutorial";
                break;

            default:
                levelName.text = currentLevel;
                break;
        }
    }
}
