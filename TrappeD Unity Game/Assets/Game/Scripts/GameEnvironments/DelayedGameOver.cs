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
			int playerAvailableLife = ps.PlayerLives;
			if(playerAvailableLife > 0 )SceneManager.LoadScene(PlayerPrefs.GetString("LAST_LEVEL"));
			else SceneManager.LoadScene ("Game Over");
			
		}
	}
}
