using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BallOutOfBoundChecker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.name == "Ball")
			SceneManager.LoadScene ("Game Over");
	}
}
