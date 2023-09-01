using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KeySystem : MonoBehaviour {


	public GameObject gameobjectToInform;

	private Transform startingPosition;

	private SpeakerManagerOne smo;


	// Use this for initialization
	void Start () {
		smo = FindObjectOfType<SpeakerManagerOne>();
		startingPosition = gameObject.transform;
	}
	
	void OnCollisionStay2D(Collision2D other){
		gameobjectToInform.SendMessage ("OnKeyTouched");
		gameObject.GetComponent<Renderer>().material.color = Color.green;

		if (SceneManager.GetActiveScene().name.Equals("Level_06_RC", System.StringComparison.OrdinalIgnoreCase))
		{
			smo.disableSpeaker();
		}

		//Debug.Log ("key touched");
		//gameObject.transform.position = new Vector3 (gameObject.transform.position.x, keyDownLimit.position.y, gameObject.transform.position.z);
	}

	void OnCollisionExit2D(Collision2D other){
		
		if (!SceneManager.GetActiveScene().name.Equals("Level_06_RC", System.StringComparison.OrdinalIgnoreCase))
        {
			gameobjectToInform.SendMessage("OnKeyUntouched");
		}

		gameObject.GetComponent<Renderer>().material.color = Color.white;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.GetComponent<Collider2D>().gameObject.name == "Ball")gameobjectToInform.SendMessage ("OnKeyTouchedByPlayer");
	}


}
