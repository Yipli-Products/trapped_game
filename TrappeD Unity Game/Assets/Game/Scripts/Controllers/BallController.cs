using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace UnitySampleAssets.CrossPlatformInput.PlatformSpecific
{
	public class BallController : MonoBehaviour {

		[SerializeField] PlayerStats ps;
		[SerializeField] GameObject pauseCanvas;

		public float hInput=0.0f;
		public float vInput=0.0f;


		//Vars for ground check
		public bool isGrounded = false;
		public Transform groundCheck;
		public float groundRadius = 5.0f;
		public LayerMask whatIsGround;



		public float horizontalForce = 15.0f;
		public float verticalForce = 200.0f;

		public bool moveHorzRight = false;
		public bool moveHorzLeft = false;
		public bool ballJump = false;

		public float verticleMultiplier = 100f;

		//public GameObject rayDrawingPosition;
		//public GameObject rayDrawingEndPosition;
		//public LayerMask ignoreLayerMask;
		//private float rayDist = 0.0f;

		public float distG = 1.50f;

		Vector3 groundCheckPos = new Vector3(0,0,0);



		public bool isPlayerDead = false;

		public Camera mainCamera;

		public GameObject[] ballBrokenParts;


		public Text coinText;
		private int coinAmount = 0;
		private int totalCoins;

		public float buttonHoldDownTime = 0;

		private float horzontalInputMultiplier = 0.0f;

	

		public AudioClip jumpingSound;



		public Image Life1;
		public Image Life2;
		public Image Life3;

		public GameObject dustParticleLeftToRight;
		public GameObject dustParticleRightToLeft;
		public float xOffsetLeftToRight = 0.3f;
		public float xOffsetRightToLeft = 0.3f;
		public float yOffset = -0.55f;

		private SpriteRenderer thisBallSpriteRenderer;

		public Sprite[] balls;

		private GameObject backMusic;

		private bool AutoMovement = false;
		private bool MatMovement = false;
		private float backValue = 0f;
		public float forwardForce = 500f;

		string FMResponseCount = "";
		private int thisLevelPoints;

		public bool leftJump = false;

		public float waitTimeCal = 0f;
		public bool calWaitTime = true;
		public bool jumpManipulated = false;

		// Use this for initialization
		void Start () {

			//LOAD THE ACTIVE BALL
			thisBallSpriteRenderer = GetComponent<SpriteRenderer>();
			thisBallSpriteRenderer.sprite = balls[PlayerPrefs.GetInt("ACTIVE_BALL")];

				backMusic = GameObject.Find ("Main Camera");

		
			coinAmount = PlayerPrefs.GetInt ("Coins");
			coinText.text = coinAmount.ToString();
			int playerLife = PlayerPrefs.GetInt ("PLAYER_LIFE");
			if (playerLife < 3) {
					gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("CHKP_X"), PlayerPrefs.GetFloat("CHKP_Y"), PlayerPrefs.GetFloat("CHKP_Z"));
			}

			if (SceneManager.GetActiveScene().ToString() == "Level_04_RC")
			{
				transform.position = new Vector3(1.1f, 3f, -0.8f);
			}
		
			showLifeOnScreen ();
			#if UNITY_ANDROID
				horzontalInputMultiplier = 7000.0f;
			#endif

			#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
				horzontalInputMultiplier = 220.0f;

#endif

			thisLevelPoints = 0;
			PlayerPrefs.SetInt("thisLevelPoints", thisLevelPoints);

			totalCoins = ps.GetCoinScore();
		}



		private void showLifeOnScreen(){
				int playerLife = PlayerPrefs.GetInt ("PLAYER_LIFE");
				if (playerLife == 2)
				Life3.enabled = false;
			else if (playerLife == 1) {
				Life2.enabled = false;
				Life3.enabled = false;
			} else if (playerLife == 0) {
				Life1.enabled = false;
				Life2.enabled = false;
				Life3.enabled = false;
			}
		}

		/*public void touchJump(int j){
			if (j == 1) {
					ballJump = true;
					GetComponent<AudioSource>().PlayOneShot(jumpingSound);
				}
			else
				ballJump = false;
		}*/

		public void touchHorizontalMoveDown(int h){
			if (h == 1) {
					//buttonHoldDownTime += Time.deltaTime;
					buttonHoldDownTime = 0.009f;
					moveHorzRight = true;
				} /*else {
					buttonHoldDownTime = 0;
					moveHorzRight = false;
				}*/
			if (h == -1) {
					//buttonHoldDownTime += Time.deltaTime;
					buttonHoldDownTime = 0.002f;
					moveHorzLeft = true;
				} /*else {

					moveHorzLeft = false;
				}*/

			if (h == 2)
			{
				buttonHoldDownTime = 0.009f;
				moveHorzRight = true;
			}

			if (h == 3)
			{
				buttonHoldDownTime = 0.002f;
				moveHorzLeft = true;
			}
		}

			public void touchHorizontalMoveUp(int k){
				buttonHoldDownTime = 0;
				moveHorzLeft = false;
				moveHorzRight = false;
			}


		// Update is called once per frame
		void FixedUpdate () {

			isGrounded = Physics2D.OverlapCircle (groundCheckPos, 0.01f, whatIsGround);


#if UNITY_ANDROID
				//hInput = CrossPlatformInputManager.GetAxis ("Horizontal");
				//ManageAndroidActions();
#endif

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
			
			//ManagePlayerInput();
			
#endif
			ManagePlayerInput();
			//ManageAndroidActions();

			hInput *= horizontalForce;

			if (isPlayerDead == false) {
				//GetComponent<Rigidbody2D>().AddForce (new Vector2 (hInput*Time.deltaTime*horzontalInputMultiplier, 0));
				GetComponent<Rigidbody2D>().AddForce(new Vector2(hInput * Time.deltaTime, 0));

				if (isGrounded && ballJump) {
					//GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, verticalForce*Time.deltaTime*60.0f)); //o

					//GetComponent<Rigidbody2D>().AddForce(new Vector2(verticalForce * Time.deltaTime * 60.0f, verticalForce * Time.deltaTime * 60.0f));

					if (!leftJump)
					{
						GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 20f);
						//StartCoroutine(positiveXJump());
					}
					else if (leftJump)
					{
						GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 20f);
						//StartCoroutine(negetiveXJump());
					}
					else
					{
						print("jumping when nothing is true");
						GetComponent<Rigidbody2D>().AddForce(new Vector2(0, verticalForce * Time.deltaTime * 60.0f)); //o
					}

					AddJumpPlayerAction();
				}
			}


			if (GetComponent<Rigidbody2D>().velocity.y > 8.0f)
				GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, 8.0f); 
			if (GetComponent<Rigidbody2D>().velocity.x > 5.0f)
				GetComponent<Rigidbody2D>().velocity = new Vector2 (5.0f, GetComponent<Rigidbody2D>().velocity.y);
			if (GetComponent<Rigidbody2D>().velocity.x < -5.0f)
				GetComponent<Rigidbody2D>().velocity = new Vector2 (-5.0f, GetComponent<Rigidbody2D>().velocity.y);





		}

		private IEnumerator positiveXJump()
		{
			yield return new WaitForSeconds(0.05f);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(verticalForce * Time.deltaTime * verticleMultiplier * 3, 0f));
		}

		private IEnumerator negetiveXJump()
		{
			yield return new WaitForSeconds(0.05f);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(-verticalForce * Time.deltaTime * verticleMultiplier * 3, 0f));
		}

		void Update(){

			manageAUtoMovement();
			//manageMatAutoMovement();
			ManageMatActions();

			if (isPlayerDead)GetComponent<Rigidbody2D>().velocity = new Vector3 (0,0,0);

			#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
				if (Input.GetKeyDown (KeyCode.UpArrow)){
					//ballJump = true;
					GetComponent<AudioSource>().PlayOneShot(jumpingSound);
				}
				if (Input.GetKeyUp (KeyCode.UpArrow))
					ballJump = false;
			#endif

			groundCheckPos = new Vector3 (transform.position.x, transform.position.y-distG, transform.position.z);


			if (isGrounded) {
				if (GetComponent<Rigidbody2D>().velocity.x > 0.1) {
					GameObject go = Instantiate (dustParticleLeftToRight, new Vector2 (gameObject.transform.position.x + xOffsetLeftToRight, gameObject.transform.position.y + yOffset), dustParticleLeftToRight.transform.rotation) as GameObject;
					Destroy (go, .60f);
				} else if (GetComponent<Rigidbody2D>().velocity.x < -0.1) {
					GameObject go = Instantiate (dustParticleRightToLeft, new Vector2 (gameObject.transform.position.x + xOffsetRightToLeft, gameObject.transform.position.y + yOffset), dustParticleRightToLeft.transform.rotation) as GameObject;
					Destroy (go, .60f);
				}
			}

			if (calWaitTime)
			{
				waitTimeCal += Time.deltaTime;
			}

			if (ballJump)
			{
				if (jumpManipulated)
				{
					if (!leftJump)
					{
						GetComponent<Rigidbody2D>().AddForce(new Vector2(10f, 0f), ForceMode2D.Force);
					}
					else if (leftJump)
					{
						GetComponent<Rigidbody2D>().AddForce(new Vector2(-10f, 0f), ForceMode2D.Force);
					}
				}
				else
				{
					if (!leftJump)
					{
						GetComponent<Rigidbody2D>().AddForce(new Vector2(40f, 0f), ForceMode2D.Force);
					}
					else if (leftJump)
					{
						GetComponent<Rigidbody2D>().AddForce(new Vector2(-40f, 0f), ForceMode2D.Force);
					}
				}
			}

		}

		public void PlayerDead(){
				//Debug.Log ("Someone called player Dead...");
			PlayerPrefs.SetString ("LAST_LEVEL", Application.loadedLevelName);
			PlayerPrefs.SetInt ("PLAYER_LIFE", PlayerPrefs.GetInt("PLAYER_LIFE") - 1);

			GetComponent<Rigidbody2D>().velocity = new Vector3 (0,0,0);
			isPlayerDead = true;
			mainCamera.SendMessage ("StopFollowingCameraToBall");
		
				backMusic.GetComponent<AudioSource>().Stop ();

			Instantiate (ballBrokenParts[PlayerPrefs.GetInt("ACTIVE_BALL")], gameObject.transform.position, Quaternion.identity);
			Destroy (gameObject);
		}

		public void AddCoins(int amount){
			coinAmount += amount;
			thisLevelPoints += amount;
			totalCoins += amount;
			coinText.text = coinAmount.ToString ();
			
			PlayerPrefs.SetInt ("Coins", coinAmount);
			PlayerPrefs.SetInt("thisLevelPoints", thisLevelPoints);

			ps.SetCoinScore(totalCoins);
		}


		public void DecreaseLifeByOne(){
			PlayerPrefs.SetInt ("PLAYER_LIFE", PlayerPrefs.GetInt("PLAYER_LIFE") - 1);
			showLifeOnScreen ();
		}

		private void ManagePlayerInput()
		{
			hInput = Input.GetAxis("Horizontal"); //FOR PC

			if (Input.GetAxis("Horizontal") > 0f)
			{
				//AutoMovement = false;
				//hInput = Input.GetAxis("Horizontal"); //FOR PC
				hInput = forwardForce;
				leftJump = false;
				calWaitTime = false;
				waitTimeCal = 0;
			}
			else
			{
				//AutoMovement = true;
				if (!ballJump)
				{
					calWaitTime = true;
				}
				
				if (waitTimeCal >= 5f)
				{
					hInput = -8f;
					leftJump = true;
				}
			}
		}

		private void manageAUtoMovement ()
		{
			if (AutoMovement)
			{
				backValue -= 0.0003f;
			}
			else
			{
				backValue = 0f;
			}
		}

		/*private void manageMatAutoMovement ()
		{
			if (MatMovement)
			{
				forwardValue += 0.0005f;
			}
			else
			{
				forwardValue = 0f;
			}
		} */

		private void ManageAndroidActions () {
			if (moveHorzRight)
				hInput = buttonHoldDownTime;
			else if (moveHorzLeft)
				hInput = -buttonHoldDownTime;
			else hInput = 0.0f;
		}

		private void AddJumpPlayerAction() 
		{
			PlayerSession.Instance.AddPlayerAction(PlayerSession.PlayerActions.JUMP);
		}

		private void JumpFalse () {
			ballJump = false;
		}	  

		private void ManageMatActions()
		{
			//#if UNITY_ANDROID
			string FMResponse = InitBLE.PluginClass.CallStatic<string>("_getFMResponse");
			Debug.Log("UNITY FMResponse: " + FMResponse);
			//Sample FMResponse : 
			/*
				 {response_count}.{response}
				 1.Left Move
				 2.Right Move
				 3.Jumping
				 4.Bending
			 */

			string[] FMTokens = FMResponse.Split('.');
			Debug.Log("UNITY FMTokens: " + FMTokens[0]);

			//FMResponseCount = FMTokens[0];

			if (FMTokens.Length > 1 && !FMTokens[0].Equals(FMResponseCount))
			{
				FMResponseCount = FMTokens[0];

				string[] whiteSpace = FMTokens[1].Split('+');

				if (whiteSpace.Length > 1)
				{
					//performedAction = whiteSpace[0];
					//  Runnig+23+1.2 // coming string from mat

					if (whiteSpace[0].Equals(PlayerSession.PlayerActions.RUNNING, System.StringComparison.OrdinalIgnoreCase))
					{
						int step = int.Parse(whiteSpace[1]);

						moveHorzLeft = false;
						leftJump = false;
						float hForce = 40f;
						calWaitTime = false;
						waitTimeCal = 0f;

						gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(hForce, 0f));

						PlayerSession.Instance.AddPlayerAction(PlayerSession.PlayerActions.RUNNING, step);
					}

				}
				else if (whiteSpace.Length == 1)
				{
					if (whiteSpace[0].Equals(PlayerSession.PlayerActions.JUMP, System.StringComparison.OrdinalIgnoreCase))
					{
						ballJump = true;
						GetComponent<AudioSource>().PlayOneShot(jumpingSound);

						Invoke("JumpFalse", 1f);
					}
					else if (whiteSpace[0].Equals(PlayerSession.PlayerActions.STOP, System.StringComparison.OrdinalIgnoreCase))
					{
						if (!ballJump)
						{
							calWaitTime = true;
						}

						if (waitTimeCal >= 5f)
						{
							moveHorzRight = false;
							hInput = -8f;
							leftJump = true;

							//float hForce = -1.5f;
							//gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(hForce--, 0f));
						}

						PlayerSession.Instance.AddPlayerAction("stop");
					}
				}


				/*if (FMTokens[1].Equals(PlayerSession.PlayerActions.JUMP, System.StringComparison.OrdinalIgnoreCase))
				{
					ballJump = true;
					GetComponent<AudioSource>().PlayOneShot(jumpingSound);

					Invoke("JumpFalse", 1f);
				}
				else if (FMTokens[1].Equals(PlayerSession.PlayerActions.STOP, System.StringComparison.OrdinalIgnoreCase)) {
					moveHorzRight = false;
					float hForce = -1.5f;

					gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(hForce--, 0f));

					PlayerSession.Instance.AddPlayerAction(PlayerSession.PlayerActions.STOP);
				}
				else if (FMTokens[1].Contains(PlayerSession.PlayerActions.RUNNING)) {
					moveHorzLeft = false;

					float hForce = 15f;

					gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(hForce, 0f));

					PlayerSession.Instance.AddPlayerAction(PlayerSession.PlayerActions.RUNNING);
				}*/
			}
		}

		private void leftOps()
		{
			moveHorzRight = false;
			leftJump = true;
			float hForce = -1.5f;

			gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(hForce--, 0f));
		}

		private void pauseGame()
		{
			Time.timeScale = 0f;
			pauseCanvas.SetActive(true);
		}
	}
}
