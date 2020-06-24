using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class FirstRun : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject runVideoScreen;
    [SerializeField] Text AIText;
    [SerializeField] Text speakerT;

    private BallController bc;
    public bool RTActive = false;

    // Start is called before the first frame update
    void Start()
    {
        runVideoScreen.SetActive(false);
        bc = FindObjectOfType<BallController>();

        speakerT.text = "Start Running to move Forward";
        AudioControl.Instance.playAudio();
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
            AudioControl.Instance.playAudio();

            bc.calWaitTime = false;
            bc.waitTimeCal = 0f;

            Time.timeScale = 0.1f;
            RTActive = true;
        }
    }

    public void couritineManager()
    {
        StartCoroutine(appriciatePlayer());
    }

    private IEnumerator appriciatePlayer()
    {
        yield return new WaitForSecondsRealtime(2f);

        speakerT.text = "Good one, Keep Running";
        AudioControl.Instance.playAudio();
        yield return new WaitForSecondsRealtime(1f);

        speakerT.text = "Perfect, Keep it up.";
        AudioControl.Instance.playAudio();
        yield return new WaitForSecondsRealtime(1f);

        speakerT.text = "Good one, Keep Running";
        AudioControl.Instance.playAudio();
    }

    private void RunTutorial()
    {
        bc.allowjump = false;
        bc.allowRun = true;
        bc.allowStop = false;

        /*if (bc.detectedAction == PlayerSession.PlayerActions.RUNNING)
        {
            Time.timeScale = 1f;
            bc.RunningStartAction();
        }*/
    }
}
