using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class FirstCheckpoint : MonoBehaviour
{
    //required variables
    [SerializeField] GameObject checkpointFrame;

    [SerializeField] GameObject runVideoScreen;
    [SerializeField] GameObject stopVideoScreen;
    [SerializeField] GameObject jumpVideoScreen;

    [SerializeField] GameObject VideoScreenPanel;
 
    [SerializeField] Text speakerT;

    private BallController bc;

    bool calledAnim = false;

    // Start is called before the first frame update
    void Start()
    {
        bc = FindObjectOfType<BallController>();

        checkpointFrame.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!calledAnim)
            {
                stopVideoScreen.SetActive(false);
                jumpVideoScreen.SetActive(false);
                runVideoScreen.SetActive(false);

                VideoScreenPanel.SetActive(false);

                speakerT.text = "If you die, level will resume from last checkpoint";
                AudioControl.Instance.playAudio();

                calledAnim = true;

                bc.allowjump = false;
                bc.allowRun = false;
                bc.Runbackward = false;

                Time.timeScale = 0.1f;
                StartCoroutine(frameAnimation());
            }
            else
            {
                print("Checkpoint tutorial is done already.");
            }
        }
    }

    IEnumerator frameAnimation()
    {
        checkpointFrame.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);

        checkpointFrame.SetActive(false);

        bc.allowjump = true;
        bc.allowRun = true;
        bc.Runbackward = false;

        AudioControl.Instance.playAudio();
        speakerT.text = "Keep Running";

        VideoScreenPanel.SetActive(true);

        runVideoScreen.SetActive(true);
        jumpVideoScreen.SetActive(false);
        stopVideoScreen.SetActive(false);

        Time.timeScale = 1f;
    }
}
