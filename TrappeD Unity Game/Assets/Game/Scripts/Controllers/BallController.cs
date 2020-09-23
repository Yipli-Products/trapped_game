using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using YipliFMDriverCommunication;

namespace UnitySampleAssets.CrossPlatformInput.PlatformSpecific
{
	public class BallController : MonoBehaviour {

		[SerializeField] PlayerStats ps;
		[SerializeField] GameObject pauseCanvas;

		public float hInput=0.0f;
		public float vInput=0.0f;

		float timer;

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

		int FMResponseCount = -1;
		private int thisLevelPoints;

		public bool leftJump = false;

		public float waitTimeCal = 0f;
		public bool calWaitTime = true;
		public bool jumpManipulated = false;

		PauseGame pg;

		public bool allowRun = false;
		public bool allowjump = false;
		public bool Runbackward = false;
		public bool isJumping = false;
		public bool gameHint = false;

		public string currentLevel;

		public float matBallForce = 30f;

		// Use this for initialization
		void Start () {

			currentLevel = SceneManager.GetActiveScene().name;

			pg = FindObjectOfType<PauseGame>();

			//LOAD THE ACTIVE BALL
			thisBallSpriteRenderer = GetComponent<SpriteRenderer>();
			thisBallSpriteRenderer.sprite = balls[ps.Active_ball];

			backMusic = GameObject.Find ("Main Camera");

		
			coinAmount = ps.GetCoinScore();
			coinText.text = coinAmount.ToString();
			int playerLife = PlayerPrefs.GetInt ("PLAYER_LIFE");
			if (playerLife < 3 && currentLevel != "Level_09_RC") {
				gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("CHKP_X"), 3, PlayerPrefs.GetFloat("CHKP_Z"));
			}
			else
            {
				gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("CHKP_X"), PlayerPrefs.GetFloat("CHKP_Y"), PlayerPrefs.GetFloat("CHKP_Z"));
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

			StartCoroutine(BluetoothCheck());
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

			if (!isGrounded && isJumping)
            {
				if (GetComponent<Rigidbody2D>().velocity.y < 0)
				{
					if (!leftJump)
					{
						GetComponent<Rigidbody2D>().AddForce(new Vector2(10f, 0f));
					}
					else if (leftJump)
					{
						GetComponent<Rigidbody2D>().AddForce(new Vector2(-10f, 0f));
					}
				}

				ballJump = false;
			}




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
						isJumping = true;
						GetComponent<Rigidbody2D>().AddForce(new Vector2(1000f, 1500f));
						//StartCoroutine(positiveXJump());
					}
					else if (leftJump)
					{
						isJumping = true;
						GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000f, 1500f));
						//StartCoroutine(negetiveXJump());
					}
					else
					{
						print("jumping when nothing is true");
						isJumping = true;
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

		private void CalculateTimkePlayed()
        {
			timer += Time.deltaTime;

			if (timer > 1)
            {
				ps.TimePlayed += 1;
				timer = 0;
			}
        }

		void Update(){

			CalculateTimkePlayed();

			manageAUtoMovement();
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

			/*if (ballJump && isJumping)
			{
				if (!leftJump)
				{
					GetComponent<Rigidbody2D>().AddForce(new Vector2(1000f, 0f), ForceMode2D.Force);
				}
				else if (leftJump)
				{
					GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000f, 0f), ForceMode2D.Force);
				}
			}*/

		}

		public void PlayerDead(){
				//Debug.Log ("Someone called player Dead...");
			PlayerPrefs.SetString ("LAST_LEVEL", SceneManager.GetActiveScene().name);
			PlayerPrefs.SetInt ("PLAYER_LIFE", PlayerPrefs.GetInt("PLAYER_LIFE") - 1);

			

			GetComponent<Rigidbody2D>().velocity = new Vector3 (0,0,0);
			isPlayerDead = true;
			mainCamera.SendMessage ("StopFollowingCameraToBall");
		
				backMusic.GetComponent<AudioSource>().Stop ();

			Instantiate (ballBrokenParts[ps.Active_ball], gameObject.transform.position, Quaternion.identity);
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

				Time.timeScale = 1f;

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
				
				if (waitTimeCal >= 4f)
				{
					hInput = -20f;
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

		private void ManageAndroidActions () {
			if (moveHorzRight)
				hInput = buttonHoldDownTime;
			else if (moveHorzLeft)
				hInput = -buttonHoldDownTime;
			else hInput = 0.0f;
		}

		private void AddJumpPlayerAction() 
		{
			if (currentLevel != "Level_Tutorial")
			{
				PlayerSession.Instance.AddPlayerAction(YipliUtils.PlayerActions.JUMP);
			}
		}

		private void JumpFalse () {
			ballJump = false;
		}

		private void ManageMatActions()
        {
			string fmActionData = InitBLE.PluginClass.CallStatic<string>("_getFMResponse");
			Debug.Log("Json Data from Fmdriver : " + fmActionData);

			FmDriverResponseInfo singlePlayerResponse = JsonUtility.FromJson<FmDriverResponseInfo>(fmActionData);

			if (singlePlayerResponse == null) return;

			if (FMResponseCount != singlePlayerResponse.count)
            {
				Debug.Log("FMResponse " + fmActionData);
				FMResponseCount = singlePlayerResponse.count;

				Debug.LogError("provided action id : " + singlePlayerResponse.playerdata[0].fmresponse.action_id);

				YipliUtils.PlayerActions providedAction = ActionAndGameInfoManager.GetActionEnumFromActionID(singlePlayerResponse.playerdata[0].fmresponse.action_id);

				switch(providedAction)
                {
					case YipliUtils.PlayerActions.PAUSE:
						Debug.Log("processing mat input pause.");
						pg.pauseFunction();
						break;

					case YipliUtils.PlayerActions.RUNNINGSTOPPED:
						/*if (currentLevel == "Level_Tutorial" && !Runbackward)
						{
							calWaitTime = false;
							waitTimeCal = 0f;
							return;
						}*/

						RunningStopAction();
						break;

					case YipliUtils.PlayerActions.STOP:
						/*if (currentLevel == "Level_Tutorial" && !Runbackward)
						{
							calWaitTime = false;
							waitTimeCal = 0f;
							return;
						}*/

						RunningStopAction();
						break;

					// actions
					case YipliUtils.PlayerActions.RUNNING:
						// do run here
						int steps = 1;

						Debug.LogError("from YipliUtils.PlayerActions.RUNNING case in switch");

						///CheckPoint for the running action properties.
						/*if (singlePlayerResponse.playerdata[0].fmresponse.properties.ToString() != "null")
						{
							string[] tokens = singlePlayerResponse.playerdata[0].fmresponse.properties.Split(',');

							if (tokens.Length > 0)
							{
								//Split the property value pairs:

								//{"count":240,"timestamp":1597928543171,"playerdata":[{"id":1,"fmresponse":{"action_id":"SWLO","action_name":"Running","properties":"totalStepsCount:1,speed:1.60"},"count":123},{null}]}

								string[] totalStepsCountKeyValue = tokens[0].Split(':');
								if (totalStepsCountKeyValue[0].Equals("totalStepsCount"))
								{
									Debug.Log("Adding steps : " + totalStepsCountKeyValue[1]);
									steps = int.Parse(totalStepsCountKeyValue[1]);
								}

								string[] speedKeyValue = tokens[1].Split(':');
								if (speedKeyValue[0].Equals("speed"))
								{
									//TODO : Do some handling if speed parameter needs to be used to adjust the running speed in the game.
									float speedAdditive = float.Parse(speedKeyValue[1]);
									matBallForce *= speedAdditive;
								}
							}
						}*/

						Debug.LogError("ball jump status : " + ballJump);

						if (!ballJump)
						{
							RunningStartAction();

							PlayerSession.Instance.AddPlayerAction(YipliUtils.PlayerActions.RUNNING, steps);
						}
						break;

					case YipliUtils.PlayerActions.JUMP:
						if (currentLevel == "Level_Tutorial")
						{
							FindObjectOfType<TutorialManager>().TextTutorialNextButton();
						}

						JumpAction();

						PlayerSession.Instance.AddPlayerAction(YipliUtils.PlayerActions.JUMP);
						break;
				}
			}
		}


		public void RunningStartAction()
        {
			Time.timeScale = 1f;

			moveHorzLeft = false;
            leftJump = false;

            calWaitTime = false;
            waitTimeCal = 0f;

            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(matBallForce, 0f), ForceMode2D.Impulse);
		}

        private IEnumerator BluetoothCheck()
		{
			Debug.Log("Starting the Co-routine for Bluetooth Check");
			while (true)
			{
				yield return new WaitForSeconds(0.5f);
				if (!YipliHelper.GetBleConnectionStatus().Equals("Connected", System.StringComparison.OrdinalIgnoreCase) && (Time.timeScale == 1f))
				{
					//Pause the game.
					Debug.Log("Pausing the game as the bluetooth isn't connected.");
					pg.pauseFunction();
				}
			}
		}

		public void JumpAction()
        {
			Time.timeScale = 1f;
			ballJump = true;
			GetComponent<AudioSource>().PlayOneShot(jumpingSound);

			calWaitTime = false;
			waitTimeCal = 0f;

			if (currentLevel == "Level_Tutorial")
            {
				allowRun = true;
            }
		}

		public void RunningStopAction()
        {
			Time.timeScale = 1f;
			calWaitTime = true;

			// generate the notification for not doing anything. just to let player know.
			if (waitTimeCal >= 5f)
			{
				moveHorzRight = false;
				hInput = -20f;
				leftJump = true;
			}
		}
	}
}
