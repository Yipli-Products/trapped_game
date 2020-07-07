using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWon : MonoBehaviour {

	[SerializeField] PlayerStats ps;

	public float delayTime = 10.0f;
	private float elapsedTime = 0.0f;
	private bool isGameWon = false;

	private YSessionManager ysm;

	private string currentScene;
	private int currentLevelIndex;

	// Use this for initialization
	void Start () {
		currentScene = SceneManager.GetActiveScene().name;
		ysm = FindObjectOfType<YSessionManager>();

		setCurrentLevelIndex();
	}
	
	// Update is called once per frame
	void Update () {
		if (isGameWon) {
			elapsedTime += Time.deltaTime;
			if (elapsedTime >= delayTime) SceneManager.LoadScene("Game Won");
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.name == "Ball") {
			/*if(PlayerPrefs.GetInt ("NUMBER_OF_COMP_LEVELS") < PlayerPrefs.GetInt ("CURRENT_LEVEL_SERIAL")){
				PlayerPrefs.SetInt ("NUMBER_OF_COMP_LEVELS", PlayerPrefs.GetInt ("CURRENT_LEVEL_SERIAL"));
			} */

			if (currentLevelIndex >= (ps.GetCompletedLevels() + 1))
            {
				ps.SetCompletedLevels(ps.GetCompletedLevels() + 1);

				Debug.Log("completed Levels current level index : " + currentLevelIndex);
				Debug.Log("completed Levels : " + ps.GetCompletedLevels());
			}

			if (currentScene != "Level_Tutorial")
            {
				ysm.StoreSession();
			}

			isGameWon = true;
		}
			
	}

	private void setCurrentLevelIndex()
    {
        switch (currentScene)
        {
			case "Level_Tutorial":
				currentLevelIndex = 1;
				break;

			case "Level_01_RC":
				currentLevelIndex = 2;
				break;

            case "Level_02_RC":
				currentLevelIndex = 3;
				break;

            case "Level_03_RC":
				currentLevelIndex = 4;
				break;

            case "Level_04_RC":
				currentLevelIndex = 5;
				break;

            case "Level_05_RC":
				currentLevelIndex = 6;
				break;

            case "Level_06_RC":
				currentLevelIndex = 7;
				break;

            case "Level_07_rc":
				currentLevelIndex = 8;
				break;

            case "Level_08_RC":
				currentLevelIndex = 9;
				break;

            case "Level_09_RC":
				currentLevelIndex = 10;
				break;

            default:
				currentLevelIndex = 0;
				break;
        }
    }
}
