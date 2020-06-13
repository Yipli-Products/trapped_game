using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class FirstRun : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject runVideoScreen;
    [SerializeField] Text AIText;

    private BallController bc;
    public bool RTActive = false;

    // Start is called before the first frame update
    void Start()
    {
        runVideoScreen.SetActive(false);
        bc = FindObjectOfType<BallController>();
    }

    private void Update()
    {
        if (RTActive)
        {
            RunTutorial();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            runVideoScreen.SetActive(true);
            AIText.fontSize = 40;
            AIText.text = "Run on Mat to move forward.";

            bc.calWaitTime = false;
            bc.waitTimeCal = 0f;

            Time.timeScale = 0.1f;
            RTActive = true;
        }
    }

    private void RunTutorial()
    {
        if (bc.detectedAction == PlayerSession.PlayerActions.RUNNING)
        {
            bc.RunningStartAction();
        }
    }
}
