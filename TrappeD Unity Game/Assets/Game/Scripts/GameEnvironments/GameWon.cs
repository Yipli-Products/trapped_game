using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameWon : MonoBehaviour {

	[SerializeField] PlayerStats ps;

	public float delayTime = 10.0f;
	private float elapsedTime = 0.0f;
	private bool isGameWon = false;

	private YSessionManager ysm;

	// Use this for initialization
	void Start () {
		ysm = FindObjectOfType<YSessionManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isGameWon) {
			elapsedTime += Time.deltaTime;
			if (elapsedTime >= delayTime)Application.LoadLevel ("Game Won");
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.name == "Ball") {
			if(PlayerPrefs.GetInt ("NUMBER_OF_COMP_LEVELS") < PlayerPrefs.GetInt ("CURRENT_LEVEL_SERIAL")){
				PlayerPrefs.SetInt ("NUMBER_OF_COMP_LEVELS", PlayerPrefs.GetInt ("CURRENT_LEVEL_SERIAL"));
			}

			ps.SetCompletedLevels(ps.GetCompletedLevels() + 1);
			
			if (YipliHelper.checkInternetConnection())
			{
				ysm.StoreSession();
			}

			isGameWon = true;
		}
			
	}
}
