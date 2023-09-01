using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DelayedGameOver : MonoBehaviour {

	public float delayTime = 5.0f;
	private float elapsedTime = 0.0f;

	[SerializeField] PlayerStats ps;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= delayTime) {
			//int playerAvailableLife = PlayerPrefs.GetInt("PLAYER_LIFE");
			
			if (ps.PlayerLives > 0)
			{
				ps.PlayerLives -= 1;

				ps.TimePlayed = 0;
				ps.CalBurned = 0;
				ps.FpPoints = 0;

				ps.ThisSessionTimePlayed = 0;
				ps.ThisSessionCalBurned = 0;
				ps.ThisSessionFpPoints = 0;

				SceneManager.LoadScene(PlayerPrefs.GetString("LAST_LEVEL"));
			}
			else
			{
				ps.TimePlayed = 0;
				ps.CalBurned = 0;
				ps.FpPoints = 0;

				ps.ThisSessionTimePlayed = 0;
				ps.ThisSessionCalBurned = 0;
				ps.ThisSessionFpPoints = 0;

				ps.CheckPointPassed = false;
				ps.PlayerLives = 3;
				SceneManager.LoadScene("Game Over");
			}
			
		}
	}
}
