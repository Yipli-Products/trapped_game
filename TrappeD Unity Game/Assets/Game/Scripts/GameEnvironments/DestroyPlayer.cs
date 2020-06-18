using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyPlayer : MonoBehaviour {

	public bool Shaking; 
	private float ShakeDecay;
	private float ShakeIntensity;    
	private Vector3 OriginalPos;
	private Quaternion OriginalRot;

	private GameObject cameraRef;

	public AudioClip gameOverSound;

	private YSessionManager ysm;

	private string currentScene;

	void Start()
	{
		currentScene = SceneManager.GetActiveScene().name;

		Shaking = false;
		cameraRef = GameObject.Find ("Main Camera");

		ysm = FindObjectOfType<YSessionManager>();
	}


	
	// Update is called once per frame
	void Update () {
		if(ShakeIntensity > 0)
		{
			cameraRef.transform.position = OriginalPos + Random.insideUnitSphere * ShakeIntensity;
			cameraRef.transform.rotation = new Quaternion(OriginalRot.x + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.y + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.z + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.w + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f);
			
			ShakeIntensity -= ShakeDecay;
		}
		else if (Shaking)
		{
			Shaking = false;  
		}
	}

	void OnCollisionEnter2D(Collision2D col){
			if (col.gameObject.name == "Ball") {
			   GetComponent<AudioSource>().PlayOneShot(gameOverSound);
				DoShake();

				ysm.StoreSession();

			//col.gameObject.SendMessage("PlayerDead");
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.name == "Ball") {
			DoShake();
			//			Debug.Log("Called Once from here...");

			if (currentScene != "Level_Tutorial")
			{
				ysm.StoreSession();
			}

			col.gameObject.SendMessage("PlayerDead");
			//Debug.Log("Shake the Camera");
		}
	}



	public void DoShake()
	{
		OriginalPos = cameraRef.transform.position;
		OriginalRot = cameraRef.transform.rotation;
		
		ShakeIntensity = 0.5f;
		ShakeDecay = 0.02f;
		Shaking = true;
	} 


}
