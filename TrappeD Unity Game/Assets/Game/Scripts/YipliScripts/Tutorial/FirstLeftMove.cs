using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class FirstLeftMove : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject RunAgainCol;

    [SerializeField] GameObject runVideoScreen;
    [SerializeField] GameObject jumpVideoScreen;
    [SerializeField] GameObject stopVideoScreen;

    [SerializeField] Text speakerT;
    [SerializeField] GameObject speakerBack;

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
            jumpVideoScreen.SetActive(false);
            stopVideoScreen.SetActive(true);

            bc.allowjump = false;
            bc.allowRun = false;
            bc.Runbackward = true;

            speakerBack.SetActive(true);
            StartCoroutine(TextChange());

            RunAgainCol.SetActive(true);
        }
    }

    private IEnumerator TextChange()
    {
        speakerT.text = "There could be a situation, where you must go back to jump or climb the edge.";
        AudioControl.Instance.playAudio();
        yield return new WaitForSecondsRealtime(5f);

        speakerT.text = "Stand still to move backwards.";
        AudioControl.Instance.playAudio();
        yield return new WaitForSecondsRealtime(5f);
    }
}
