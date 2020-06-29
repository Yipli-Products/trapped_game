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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            runVideoScreen.SetActive(false);
            stopVideoScreen.SetActive(true);

            bc.allowjump = false;
            bc.allowRun = false;
            bc.allowStop = true;

            AIText.fontSize = 50;
            AIText.text = "Stop Running";
            AudioControl.Instance.playAudio();

            StartCoroutine(TextChange());

            RunAgainCol.SetActive(true);
        }
    }

    private IEnumerator TextChange()
    {
        Time.timeScale = 0.2f;

        speakerT.text = "There could be a situation, where you must go back to jump or climb the edge.";
        AudioControl.Instance.playAudio();
        yield return new WaitForSecondsRealtime(5f);

        speakerT.text = "Stand still to move backwards.";
        AudioControl.Instance.playAudio();
        yield return new WaitForSecondsRealtime(5f);

        Time.timeScale = 1f;
    }
}
