using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class FirstRun : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject runVideoScreen;
    [SerializeField] GameObject jumpVideoScreen;
    [SerializeField] Text speakerT;
    [SerializeField] GameObject speakerBack;

    private BallController bc;

    private void Awake()
    {
        speakerT.text = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        speakerBack.SetActive(false);
        jumpVideoScreen.SetActive(false);

        runVideoScreen.SetActive(false);
        bc = FindObjectOfType<BallController>();

        speakerT.text = "";
        AudioControl.Instance.playAudio();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            speakerBack.SetActive(true);

            runVideoScreen.SetActive(true);
            speakerT.text = "Run to move Forward";
            AudioControl.Instance.playAudio();

            bc.calWaitTime = false;
            bc.waitTimeCal = 0f;

            bc.allowjump = false;
            bc.allowRun = true;
            bc.Runbackward = false;
        }
    }
}
