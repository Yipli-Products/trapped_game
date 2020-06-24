using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class FirstLeftMove : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject RunAgainCol;

    [SerializeField] GameObject runVideoScreen;
    [SerializeField] GameObject stopVideoScreen;
    [SerializeField] Text AIText;

    [SerializeField] Text speakerT;

    private BallController bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = FindObjectOfType<BallController>();

        stopVideoScreen.SetActive(false);
        RunAgainCol.SetActive(false);
    }

    private void Update()
    {
        if (bc.waitTimeCal >= 5f)
        {
            bc.hInput = -8f;
            bc.leftJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            runVideoScreen.SetActive(false);
            stopVideoScreen.SetActive(true);

            bc.waitTimeCal = 0;
            bc.calWaitTime = true;

            AIText.fontSize = 50;
            AIText.text = "Stop Running";
            AudioControl.Instance.playAudio();

            StartCoroutine(TextChange());

            RunAgainCol.SetActive(true);
            
            //countDownText.text = "Stop Running. if you dont do anything on mat, after a while, you will start moving backwords automatically.";
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            runVideoScreen.SetActive(false);
            stopVideoScreen.SetActive(true);

            bc.waitTimeCal = 0;
            bc.calWaitTime = true;

            AIText.fontSize = 50;
            AIText.text = "Stand Still";
            AudioControl.Instance.playAudio();

            StartCoroutine(TextChange());

            RunAgainCol.SetActive(true);

            //countDownText.text = "Stop Running. if you dont do anything on mat, after a while, you will start moving backwords automatically.";
        }
    }

    private IEnumerator TextChange()
    {
        Time.timeScale = 0.2f;

        speakerT.text = "Stand still on the Mat to start moving backwards.";
        AudioControl.Instance.playAudio();
        yield return new WaitForSecondsRealtime(5f);

        speakerT.text = "There could be a situation, where you must go back to jump or climb the edge.";
        AudioControl.Instance.playAudio();
        yield return new WaitForSecondsRealtime(5f);

        speakerT.text = "Stand still. After a while you will start going back and you will be able to jump backwards.";
        AudioControl.Instance.playAudio();
        yield return new WaitForSecondsRealtime(5f);

        Time.timeScale = 1f;
    }
}
